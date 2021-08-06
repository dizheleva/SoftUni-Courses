using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Castle.Core.Internal;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;
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
            var sb = new StringBuilder();
            var movieDto = JsonConvert.DeserializeObject<List<MovieInputModel>>(jsonString);


            var movies = new List<Movie>();

            foreach (var currentMovie in movieDto)
            {

                if (currentMovie.Title.IsNullOrEmpty() || currentMovie.Director.IsNullOrEmpty() ||
                currentMovie.Genre.IsNullOrEmpty())
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (currentMovie.Title.Length < 3 || currentMovie.Title.Length > 20)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (currentMovie.Rating < 1 || currentMovie.Rating > 10)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (currentMovie.Director.Length < 3 || currentMovie.Director.Length > 20)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                if (movies.Exists(m => m.Title == currentMovie.Title))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;

                }
                var movie = new Movie()
                {
                    Director = currentMovie.Director,
                    Duration = TimeSpan.ParseExact(currentMovie.Duration, @"hh\:mm\:ss", CultureInfo.InvariantCulture),
                    Genre = Enum.Parse<Genre>(currentMovie.Genre),
                    Rating = currentMovie.Rating,
                    Title = currentMovie.Title,
                };

                movies.Add(movie);

                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre,
                    movie.Rating.ToString("f2")));
            }
            context.Movies.AddRange(movies);

            context.SaveChanges();

            return sb.ToString().Trim();


        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ProjectionInputModel>), new XmlRootAttribute("Projections"));

            //var namespaces = new XmlSerializerNamespaces();

            //namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var projectionDtos = (List<ProjectionInputModel>)serializer.Deserialize(reader);

                var projections = new List<Projection>();

                foreach (var currentProjection in projectionDtos)
                {

                    var movie = context.Movies.FirstOrDefault(m => m.Id == currentProjection.MovieId);

                    if (movie == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    DateTime dt;

                    var isDateTimeValid = DateTime.TryParseExact(currentProjection.DateTime,
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

                    if (!isDateTimeValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var projection = new Projection()
                    {
                        Movie = movie,
                        MovieId = currentProjection.MovieId,
                        DateTime = dt,


                    };

                    projections.Add(projection);

                    sb.AppendLine(
                        string.Format(SuccessfulImportProjection, projection.Movie.Title,
                            projection.DateTime.ToString("MM/dd/yyyy")));
                }

                context.Projections.AddRange(projections);

                context.SaveChanges();

                return sb.ToString().Trim();
            }

        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<CustomersInputModel>), new XmlRootAttribute("Customers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var customerDto = (List<CustomersInputModel>)serializer.Deserialize(reader);

                var customers = new List<Customer>();

                foreach (var currentCustomer in customerDto)
                {
                    if (currentCustomer.FirstName.Length < 3 || currentCustomer.FirstName.Length > 20)
                    {
                        sb.AppendLine(ErrorMessage);
                            continue;
                    }

                    if (currentCustomer.LastName.Length < 3 || currentCustomer.LastName.Length > 20)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (currentCustomer.Age < 12 || currentCustomer.Age > 110)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    if (currentCustomer.Balance < 0.01M)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var customer = new Customer
                    {
                        Age = currentCustomer.Age,
                        Balance = currentCustomer.Balance,
                        FirstName = currentCustomer.FirstName,
                        LastName = currentCustomer.LastName
                    };

                    foreach (var ticketDto in currentCustomer.Tickets)
                    {


                        var projection = context.Projections.FirstOrDefault(p => p.Id == ticketDto.ProjectionId);

                        if (projection == null)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        if (ticketDto.Price <  0.01M)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        var ticket = new Ticket
                        {
                            Customer = customer,
                            Price = ticketDto.Price,
                            ProjectionId = ticketDto.ProjectionId,
                            CustomerId = customer.Id,
                            Projection = projection,
                        };

                        customer.Tickets.Add(ticket);
                    }

                    customers.Add(customer);

                    sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName,
                        customer.LastName, customer.Tickets.Count));
                }

                context.Customers.AddRange(customers);

                context.SaveChanges();

                return sb.ToString().Trim();
            }

        }

       

    }
}