namespace BookShop.DataProcessor
{
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Data;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(BookInputModel[]), new XmlRootAttribute("Books"));
            var textReader = new StringReader(xmlString);

            var booksDto = xmlSerializer.Deserialize(textReader) as BookInputModel[];

            var books = new List<Book>();
            
            var sb = new StringBuilder();

            foreach (var book in booksDto)
            {
                
                DateTime publishedOn; // a variable to keep the parsed value of the date
                var isDateValid = DateTime.TryParseExact(book.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out publishedOn);

                if (!IsValid(book))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var validBook = new Book
                {
                    Name = book.Name,
                    Genre = (Genre) book.Genre,
                    Price = book.Price,
                    Pages = book.Pages,
                    PublishedOn = publishedOn
                };

                books.Add(validBook);

                sb.AppendLine(string.Format(SuccessfullyImportedBook, validBook.Name, validBook.Price));
            }

            context.Books.AddRange(books);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var authorDtos = JsonConvert.DeserializeObject<AuthorInputModel[]>(jsonString);

            var authors = new List<Author>();

            var sb = new StringBuilder();

            foreach (var authorDto in authorDtos)
            {
                if (!IsValid(authorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (authors.Any(a => a.Email == authorDto.Email))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName,
                    Email = authorDto.Email,
                    Phone = authorDto.Phone
                };

                foreach (var authorBookDto in authorDto.Books)
                {
                    var book = context.Books.FirstOrDefault(b => b.Id == authorBookDto.Id);

                    if (book == null)
                    {
                        continue;
                    }

                    author.AuthorsBooks.Add(new AuthorBook
                    {
                        Author = author,
                        Book = book
                    });
                }

                if (author.AuthorsBooks.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                authors.Add(author);

                var authorName = author.FirstName + " " + author.LastName;
                var booksCount = author.AuthorsBooks.Count;

                sb.AppendLine(string.Format(SuccessfullyImportedAuthor, authorName, booksCount));
            }

            context.Authors.AddRange(authors);
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