using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BookShopSystem.Models;

namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookShopSystem.Models;
    using BookShopSystem.Data;

    public sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
            if (context.Authors.Any())
            {
                return;
            }

            Random random = new Random();

            //seeding some authors
            using (var reader = new StreamReader("authors.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 2);
                    var firstName = data[0];
                    var lastName = data[1];
                    context.Authors.AddOrUpdate(new Author()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    });

                    line = reader.ReadLine();
                }
                context.SaveChanges();
            }
            

            //seeding some categories
            using (var reader = new StreamReader("categories.txt"))
            {
                var name = reader.ReadLine();
                //line = reader.ReadLine();
                while (name != null)
                {
                    context.Categories.AddOrUpdate(new Category()
                    {
                        Name = name
                    });
                    name = reader.ReadLine();
                }
                context.SaveChanges();
            }

            
            //seeding some books
            var authors = (from a in context.Authors select a).ToArray();
            var categories = (from c in context.Categories select c).ToArray();
            //по-добре така:
            //var autors = context.Authors.ToList();
            //var categories = context.Categories.ToList();

            using (var reader = new StreamReader("books.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 6);
                    var authorIndex = random.Next(0, authors.Length);
                    var author = authors[authorIndex];
                    var edition = (Edition)int.Parse(data[0]);
                    var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var copies = int.Parse(data[2]);
                    var price = decimal.Parse(data[3]);
                    var ageRestriction = (AgeRestriction)int.Parse(data[4]);
                    var title = data[5];
                    var categoryIndex = random.Next(0, categories.Length);
                    var category = categories[categoryIndex];
                    context.Books.AddOrUpdate(new Book()
                    {
                        Author = author,
                        Edition = edition,
                        ReleaseDate = releaseDate,
                        Copies = copies,
                        Price = price,
                        AgeRestriction = ageRestriction,
                        Title = title,
                        Categories = new HashSet<Category>() {category}
                    });

                    line = reader.ReadLine();
                }
                context.SaveChanges();
            }
            

        }
    }
}
