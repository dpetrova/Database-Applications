using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using ProductsShop.Models;

namespace ProductsShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ProductsShop.Data.ProductsShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ProductsShop.Data.ProductsShopContext";
        }

        protected override void Seed(ProductsShop.Data.ProductsShopContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
           if (context.Products.Any())
           {
               return;
           }

            Random random = new Random();

            //seeding some users
            var xmlElements = XElement.Load("../../../users.xml").Elements();
            foreach (var element in xmlElements)
            {
                var user = new User();
                var fname = element.FirstAttribute;
                if (fname != null)
                {
                    user.FirstName = fname.Value;
                }
                var lname = element.Attribute("last-name");
                if (lname != null)
                {
                    user.LastName = lname.Value;
                }
                var age = element.Attribute("age");
                if (age != null)
                {
                    user.Age = int.Parse(age.Value);
                }
                
                context.Users.AddOrUpdate(user);
            }
            context.SaveChanges();
            


            //seeding some categories
            using (StreamReader json = File.OpenText("../../../categories.json"))
            {
                Category[] categories = JsonConvert.DeserializeObject<Category[]>(json.ReadToEnd());

                foreach (var category in categories)
                {
                    context.Categories.AddOrUpdate(category);
                }

                context.SaveChanges();
            }
            

            ////seeding some products
            var allUsers = context.Users.ToArray();
            var allCategories = context.Categories.ToArray();

            using (StreamReader json = File.OpenText("../../../products.json"))
            {
                Product[] products = JsonConvert.DeserializeObject<Product[]>(json.ReadToEnd());
                foreach (var product in products)
                {
                    int buyerId = random.Next(0, xmlElements.Count());
                    var buyer = allUsers[buyerId];
                    int sellerId = random.Next(0, xmlElements.Count());
                    var seller = allUsers[sellerId];
                    var randomCategories = new Category[random.Next(1, 4)];
                    for (int i = 0; i < randomCategories.Length; i++)
                    {
                        var categoryIndex = random.Next(0, allCategories.Length);
                        var category = allCategories[categoryIndex];
                        randomCategories[i] = category;
                    }
                    product.Buyer = buyer;
                    product.Seller = seller;
                    product.Categories = randomCategories;
                    
                    context.Products.AddOrUpdate(product);
                }
                context.SaveChanges();
            }
        }
    }
}
