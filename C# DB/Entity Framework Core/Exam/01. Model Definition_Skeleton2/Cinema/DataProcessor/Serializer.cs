using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Cinema.DataProcessor.ExportDto;
using Newtonsoft.Json;

namespace Cinema.DataProcessor
{
    using System;

    using Data;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context
                .Movies
                .ToArray()
                .Where(p => p.Projections.Any(t => t.Tickets.Any()) && p.Rating >= rating)
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(e => e.Projections.Sum(p => p.Tickets.Sum(t => t.Price)))
                .ToArray()
                .Select(e => new
                {
                    MovieName = e.Title,
                    Rating = e.Rating.ToString("F2"),
                    TotalIncomes = e.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("F2"),
                    Customers = context.Tickets
                        .Where(t => t.Projection.Movie.Title == e.Title)
                        .ToArray()
                        .Select(ta => new
                        {
                            FirstName = ta.Customer.FirstName,
                            LastName = ta.Customer.LastName,
                            Balance = ta.Customer.Balance.ToString("F2")
                        })
                        .ToArray()
                        .OrderByDescending(b => b.Balance)
                        .ThenBy(f => f.FirstName)
                        .ThenBy(l => l.LastName)
                        .ToArray()
                })
                //.OrderByDescending(p => p.Rating)
                //.ThenByDescending(p => p.TotalIncomes)
                .Take(10)
                .ToArray();

            var json = JsonConvert.SerializeObject(movies, Formatting.Indented);

            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var sb = new StringBuilder();

            var xmlSerializer = new XmlSerializer(typeof(ExportCustomersDto[]), new XmlRootAttribute("Customers"));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var customers = context
                .Customers
                .ToArray()
                .Where(p => p.Age >= age)
                .OrderByDescending(p => p.Tickets.Sum(t => t.Price))
                .Select(p => new ExportCustomersDto()
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    SpentMoney = p.Tickets.Sum(t => t.Price).ToString("F2"),
                    SpentTime = new TimeSpan(p.Tickets.Select(t => t.Projection.Movie).Sum(r => r.Duration.Ticks)).ToString(@"hh\:mm\:ss")
                })
                .ToArray()
                
                .Take(10)
                .ToArray();

            using var stringWriter = new StringWriter(sb);
            xmlSerializer.Serialize(stringWriter, customers, namespaces);

            return sb.ToString().TrimEnd();
        }
    }
}