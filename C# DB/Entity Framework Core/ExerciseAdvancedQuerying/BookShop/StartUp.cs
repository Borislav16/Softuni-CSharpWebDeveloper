namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using static System.Reflection.Metadata.BlobBuilder;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            Console.WriteLine(RemoveBooks(db));
        }
        // 02
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not a valid are restriction";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title,
                })
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title}"));
        }

        //03
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .Select(b => new
                {
                    b.Title,
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title}"));
        }

        //04
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Title,
                    b.Price,
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - ${b.Price:F2}"));
        }

        //05
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title}"));

        }

        //06
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            List<string> categories = input
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var books = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title}"));

        }

        //07
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime parsedTime = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < parsedTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}"));

        }

        //08
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,

                })
                .OrderBy(b => b.FullName)
                .ToList();


            return string.Join(Environment.NewLine, authors.Select(a => $"{a.FullName}"));

        }

        //09
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {

            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title}"));

        }

        //10
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Include(b => b.Author)
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.Title,
                    FullAuthorName = b.Author.FirstName + " " + b.Author.LastName,
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} ({b.FullAuthorName})"));
        }

        //11
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int books = context.Books.Count(b => b.Title.Length > lengthCheck);

            return books;

        }

        //12
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    TotalBooksByAuthor = a.Books.Sum(b => b.Copies),
                })
                .OrderByDescending(a => a.TotalBooksByAuthor)
                .ToList();


            return string.Join(Environment.NewLine, authors.Select(a => $"{a.FullName} - {a.TotalBooksByAuthor}"));

        }

        //13
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoriesProfit = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.Profit)
                .ToList();

            return string.Join(Environment.NewLine, categoriesProfit.Select(c => $"{c.Name} ${c.Profit}"));

        }

        //14
        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    TopBooks = c.CategoryBooks.OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        Year = cb.Book.ReleaseDate.Value.Year,
                        cb.Book.Title,
                    })
                    .ToList(),
                })
                .OrderBy(c => c.Name)
                .ToList();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var category in categories)
            {
                stringBuilder.AppendLine($"--{category.Name}");
                foreach (var book in category.TopBooks)
                {
                    stringBuilder.AppendLine($"{book.Title} ({book.Year})");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        //15
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        //16
        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.RemoveRange(books);

            context.SaveChanges();

            return books.Count;
        }
    }
}


