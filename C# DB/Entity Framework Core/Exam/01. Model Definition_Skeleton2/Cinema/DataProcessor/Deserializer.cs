using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Cinema.DataProcessor
{
    using System;

    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDtos = JsonConvert.DeserializeObject<ImportMoviesModel[]>(jsonString);

            var movies = new List<Movie>();

            var sb = new StringBuilder();

            foreach (var movieDto in moviesDtos)
            {
                if (!IsValid(movieDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                TimeSpan.TryParseExact(movieDto.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None, out var duration);

                var movie = new Movie()
                {
                    Title = movieDto.Title,
                    Genre = Enum.Parse<Genre>(movieDto.Genre),
                    Duration = duration,
                    Rating = movieDto.Rating,
                    Director = movieDto.Director
                };

                if (movies.Any(m => m.Title == movie.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                movies.Add(movie);

                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre.ToString(), movie.Rating.ToString("F2")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ImportProjectionsDto[]), new XmlRootAttribute("Projections"));

            var projections = new List<Projection>();

            using var stringReader = new StringReader(xmlString);
            var projectionDtos = (ImportProjectionsDto[])xmlSerializer.Deserialize(stringReader);

            foreach (var projectionDto in projectionDtos)
            {
                DateTime.TryParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var datetime);
                
                if (!IsValid(projectionDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!context.Movies.Any(m => m.Id == projectionDto.MovieId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection()
                {
                    MovieId = projectionDto.MovieId,
                    Movie = context.Movies.First(m => m.Id == projectionDto.MovieId),
                    DateTime = datetime
                };

                projections.Add(projection);

                sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, projection.DateTime.ToString("MM/dd/yyyy")));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ImportCustomersTicket[]), new XmlRootAttribute("Customers"));

            var customers = new List<Customer>();

            using var stringReader = new StringReader(xmlString);
            var customersDtos = (ImportCustomersTicket[])xmlSerializer.Deserialize(stringReader);

            foreach (var customerDto in customersDtos)
            {
                if (!IsValid(customerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance
                };

                foreach (var ticketDto in customerDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!context.Projections.Any(p => p.Id == ticketDto.ProjectionId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    customer.Tickets.Add(new Ticket()
                    {
                        Price = ticketDto.Price,
                        CustomerId = customer.Id,
                        Customer = customer,
                        ProjectionId = ticketDto.ProjectionId,
                        Projection = context.Projections.First(p=>p.Id==ticketDto.ProjectionId)
                    });
                }

                customers.Add(customer);

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}