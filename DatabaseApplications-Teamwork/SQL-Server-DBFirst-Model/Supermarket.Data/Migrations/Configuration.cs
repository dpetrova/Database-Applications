using System.Collections.Generic;
using Supermarket.Models;

namespace Supermarket.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Supermarket.Data.SupermarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Supermarket.Data.SupermarketContext";
        }

        protected override void Seed(SupermarketContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
            //if (context.Categories.Any())
            //{
            //    return;
            //}
            
            //Category beer = new Category()
            //{
            //    Id = 1,
            //    Name = "beer",
            //    Description = "this category contains various kinds of beers"
            //};

            //Category diary = new Category()
            //{
            //    Id = 2,
            //    Name = "diary",
            //    Description = "this category contains various kinds of diary products"
            //};

            //Category bread = new Category()
            //{
            //    Id = 3,
            //    Name = "bread",
            //    Description = "this category contains various kinds of breads"
            //};

            //Customer donVito = new Customer()
            //{
            //    Id = 1,
            //    Name = "pizza Don Vito",
            //    Address = "Sofia, bul.Kl.Ohridski 45",
            //    Phone = "+359899132456"
            //};

            //Customer oShipka = new Customer()
            //{
            //    Id = 2,
            //    Name = "bar OShipka!",
            //    Address = "Sofia, San Stefano 12",
            //    Phone = "+359888111222"
            //};

            //Measure kg = new Measure()
            //{
            //    Id = 1,
            //    Name = "kilogram"
            //};

            //Measure litre = new Measure()
            //{
            //    Id = 2,
            //    Name = "litre"
            //};

            //Order order11072015 = new Order()
            //{
            //    Id = 1,
            //    Quantity = 5,
            //    Discount = 0.2m,
            //    Date = new DateTime(2015, 07, 11),
            //    ProductId = 3,
            //    CustomerId = 1
            //};

            //Order order20072015 = new Order()
            //{
            //    Id = 2,
            //    Quantity = 150,
            //    Discount = 5.0m,
            //    Date = new DateTime(2015, 07, 20),
            //    ProductId = 1,
            //    CustomerId = 2
            //};

            //Order order27072015 = new Order()
            //{
            //    Id = 3,
            //    Quantity = 60,
            //    Discount = 0.8m,
            //    Date = new DateTime(2015, 07, 27),
            //    ProductId = 2,
            //    CustomerId = 2
            //};

            //Product heineken = new Product()
            //{
            //    Id = 1,
            //    Name = "Heineken",
            //    Price = 1.8m,
            //    SupplierId = 2,
            //    CategoryId = 1,
            //    MeasureId = 2
            //};

            //Product tuborg = new Product()
            //{
            //    Id = 2,
            //    Name = "Tuborg",
            //    Price = 1.5m,
            //    SupplierId = 2,
            //    CategoryId = 1,
            //    MeasureId = 2
            //};

            //Product verea = new Product()
            //{
            //    Id = 3,
            //    Name = "White cheese Verea",
            //    Price = 5.5m,
            //    SupplierId = 1,
            //    CategoryId = 2,
            //    MeasureId = 1
            //};

            //Supplier bioFarm = new Supplier()
            //{
            //    Id = 1,
            //    Name = "Bio Farm Happy Cows",
            //    Address = "Dobroslavtzi vilage 112",
            //    Phone = "+35988112233"
            //};

            //Supplier beveragesDestributor = new Supplier()
            //{
            //    Id = 2,
            //    Name = "Bevarages distributor",
            //    Address = "Sofia, bul.Tzarigradsko shosse 209",
            //    Phone = "+35988444777"
            //};

            //context.Categories.AddOrUpdate(beer, diary, bread);
            //context.Products.AddOrUpdate(heineken, tuborg, verea);
            //context.Customers.AddOrUpdate(donVito, oShipka);
            //context.Measures.AddOrUpdate(kg, litre);
            //context.Orders.AddOrUpdate(order11072015, order20072015, order27072015);
            //context.Suppliers.AddOrUpdate(bioFarm, beveragesDestributor);

            //context.SaveChanges();

        }
    }
}