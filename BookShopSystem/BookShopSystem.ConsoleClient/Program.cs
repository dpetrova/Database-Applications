using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopSystem.Data;
using BookShopSystem.Data.Migrations;


namespace BookShopSystem.ConsoleClient
{
    public class Program
    {
        static void Main()
        {

            //set and seed database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>());

            var context = new BookShopContext();

            var bookCount = context.Books.Count();
            Console.WriteLine(bookCount);
            

            //LINQ queries

            //1.Get all books after the year 2000. Select only their titles.
            var booksByYear = context.Books
                .Where(b => b.ReleaseDate.Year > 2000)
                .Select(b => b.Title);
            foreach (var book in booksByYear)
            {
                Console.WriteLine(book);
            }
            Console.WriteLine();


            //2.Get all authors with at least one book with release date before 1990. Select their first name and last name.
            var authorsWithBooks = context.Authors
                .Where(a => a.Books.Any(b => b.ReleaseDate.Year < 1990))
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName
                });
            foreach (var author in authorsWithBooks)
            {
                Console.WriteLine(author);
            }
            Console.WriteLine();


            //3.Get all authors, ordered by the number of their books (descending). Select their first name, last name and book count.
            var authors = context.Authors
                .OrderByDescending(a => a.Books.Count)
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    NumberOfBooks = a.Books.Count
                });
            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
            Console.WriteLine();


            //4.Get all books from author George Powell, ordered by their release date (descending), then by book title (ascending). 
            //Select the book's title, release date and copies.
            var booksByAuthor = context.Books
                .OrderByDescending(b => b.ReleaseDate)
                .ThenBy(b => b.Title)
                .Where(b => b.Author.FirstName == "George" && b.Author.LastName == "Powell")
                .Select(b => new
                {
                    b.Title,
                    b.ReleaseDate,
                    b.Copies
                });
            foreach (var book in booksByAuthor)
            {
                Console.WriteLine(book);
            }
            Console.WriteLine();


            //5.Get the most recent books by categories. The categories should be ordered by total book count.
            //Only take the top 3 most recent books from each category - ordered by date (descending), then by title (ascending).
            //Select the category name, total book count and for each book - its title and release date.
            var booksByCategories = context.Categories
                .OrderByDescending(c => c.Books.Count)
                .Select(c => new
                {
                    c.Name,
                    NumberOfBooks = c.Books.Count,
                    Books = c.Books.Select(b => new
                    {
                        b.Title,
                        b.ReleaseDate
                    })
                    .OrderByDescending(b => b.ReleaseDate)
                    .ThenBy(b => b.Title)
                    .Take(3)
                });
            foreach (var category in booksByCategories)
            {
                Console.WriteLine("--" + category.Name + ": " + category.NumberOfBooks + " books");
                var books = category.Books;
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                }
            }
            Console.WriteLine();



            //Populate table RelatedBooks

            //Query 3 books from the database and set them as related
            var selectedBooks = context.Books
                .Take(3)
                .ToList();

            selectedBooks[0].RelatedBooks.Add(selectedBooks[1]);
            selectedBooks[1].RelatedBooks.Add(selectedBooks[0]);
            selectedBooks[0].RelatedBooks.Add(selectedBooks[2]);
            selectedBooks[2].RelatedBooks.Add(selectedBooks[0]);

            context.SaveChanges();

            //query the first 3 books
            var booksFromQuery = context.Books
                .Take(3)
                .Select(b => new
                {
                    b.Title,
                    RelatedBooks = b.RelatedBooks.Select(rb => rb.Title)
                });
            foreach (var book in booksFromQuery)
            {
                Console.WriteLine("--{0}", book.Title);
                foreach (var relatedBook in book.RelatedBooks)
                {
                    Console.WriteLine(relatedBook);
                }
            }

        }
    }
}
