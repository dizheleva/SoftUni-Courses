using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BookShop
{
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            // 01. Age Restriction
            //Console.WriteLine(GetBooksByAgeRestriction(db, Console.ReadLine()));

            // 02. Golden Books
            //Console.WriteLine(GetGoldenBooks(db));

            // 03. Books by Price
            //Console.WriteLine(GetBooksByPrice(db));

            // 04. Not Released In
            //Console.WriteLine(GetBooksNotReleasedIn(db, int.Parse(Console.ReadLine())));

            // 05. Book Titles by Category
            //Console.WriteLine(GetBooksByCategory(db, Console.ReadLine()));

            // 06. Released Before Date
            //Console.WriteLine(GetBooksReleasedBefore(db, Console.ReadLine()));

            // 07. Author Search
            //Console.WriteLine(GetAuthorNamesEndingIn(db, Console.ReadLine()));

            // 08. Book Search
            //Console.WriteLine(GetBookTitlesContaining(db, Console.ReadLine()));

            // 09. Book Search by Author
            //Console.WriteLine(GetBooksByAuthor(db, Console.ReadLine()));

            // 10. Count Books
            //Console.WriteLine(CountBooks(db, int.Parse(Console.ReadLine())));

            // 11. Total Book Copies
            //Console.WriteLine(CountCopiesByAuthor(db));

            // 12. Profit by Category
            //Console.WriteLine(GetTotalProfitByCategory(db));

            // 13. Most Recent Books
            //Console.WriteLine(GetMostRecentBooks(db));

            // 14. Increase Prices
            //IncreasePrices(db);

            // 15. Remove Books
            //Console.WriteLine(RemoveBooks(db));
        }

        // 15. Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.Copies < 4200).ToArray();

            var count = books.Length;

            context.Books.RemoveRange(books);
            context.SaveChanges();

            return count;
        }

        // 14. Increase Prices
        public static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b => b.Price += 5);

            context.SaveChanges();
        }

        // 13. Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Books = string.Join(Environment.NewLine, c.CategoryBooks
                        .Select(cb => cb.Book)
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                        .Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))

                })
                .OrderBy(c => c.Name)
                .Select(c => $"--{c.Name}{Environment.NewLine}{c.Books}"));

        }

        // 12. Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    TotalSum = c.CategoryBooks.Select(cb => cb.Book.Price * cb.Book.Copies).Sum()
                })
                .OrderByDescending(p => p.TotalSum)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.TotalSum:F2}"));
        }

        // 11. Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Authors
                .Select(a => new
                {
                    Name = a.FirstName == null ? a.LastName : $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .Select(a => $"{a.Name} - {a.Copies}"));
        }

        // 10. Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context.Books.Count(b => b.Title.Length > lengthCheck);
        }

        // 09. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})"));
        }

        // 08. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(n => n));
        }

        // 07. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            return string.Join(Environment.NewLine, context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(n => n));
        }

        // 06. Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var parsedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.ReleaseDate < parsedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}"));
        }

        // 05. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b));
        }

        // 04. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title));
        }

        // 03. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => b.Title + " - $" + b.Price.ToString("F2")));
        }

        // 02. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title));
        }

        // 01. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t));
        }
    }
}
