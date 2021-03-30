using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ExportDto;

namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context.Authors
                    .Select(a => new
                    {
                        AuthorName = a.FirstName + " " + a.LastName,
                        Books = a.AuthorsBooks
                            .Select(ab => ab.Book)
                            .OrderByDescending(b => b.Price)
                            .Select(b => new
                            {
                                BookName = b.Name,
                                BookPrice = b.Price.ToString("F2")
                            })
                            .ToArray()
                    })
                    .ToArray()
                    .OrderByDescending(a => a.Books.Length)
                    .ThenBy(a => a.AuthorName)
                    .ToArray();

            var json = JsonConvert.SerializeObject(authors, Formatting.Indented);
            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context.Books
                .Where(b => b.PublishedOn < date && b.Genre == Genre.Science)
                .ToArray()
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Take(10)
                .Select(b => new ExportBookDto
                {
                    Name = b.Name,
                    Pages = b.Pages,
                    Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture),
                    
                })
                .ToArray();

            var xmlSerializer = new XmlSerializer(typeof(ExportBookDto[]), new XmlRootAttribute("Books"));
            var textWriter = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, books, ns);

            var xml = textWriter.ToString();
            return xml;
        }
    }
}