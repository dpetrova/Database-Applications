using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;
using Newtonsoft.Json;
using ShopSystem.Data;
using ShopSystem.Models;
using System.Xml.Linq;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using System.Text;

namespace ShopSystem.Client
{
    public class Program
    {
        public static void Main()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());
            var context = new ShopContext();
            int count = context.Products.Count();
            Console.WriteLine(count);

            // Task 2 - Seed the database
            ImportXmlUsers(context);
            ImportJsonCategories(context);
            ImportJsonProducts(context);

            // Task 3.1 - Products in range
            ExportProductsInRangeToJSON(500, 1000, "../../01.products-in-range.json", context);

            // Task 3.2 - Successfully Sold Products
            ExportSuccessfullySoldProductsToJSON("../../02.successfully-sold-products.json", context);

            // Task 3.3 - Categories By Products Count
            ExportCategoriesWithProductsCountToJSON("../../03.categories-with-products-count.json", context);

            // Task 3.4 - Users and Products
            ExportUsersAndSoldProductsToXML("../../04.users-and-products.xml", context);

            
        }

        private static void ExportUsersAndSoldProductsToXML(string filename, ShopContext context)
        {
            var users = context.Users
                .Where(u => u.Products.Any(p => p.Buyer != null))
                .Include(u => u.Products)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = u.Products
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                }).ToList();

            var settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(filename, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("users");
            writer.WriteAttributeString("count", users.Count.ToString());

            foreach (var user in users)
            {
                writer.WriteStartElement("user");

                if (user.FirstName != null)
                {
                    writer.WriteAttributeString("first-name", user.FirstName);
                }

                if (user.LastName != null)
                {
                    writer.WriteAttributeString("last-name", user.LastName);
                }

                if (user.Age != null)
                {
                    writer.WriteAttributeString("age", user.Age.ToString());
                }

                writer.WriteStartElement("sold-products");
                writer.WriteAttributeString("count", user.SoldProducts.Count().ToString());

                foreach (var soldProduct in user.SoldProducts)
                {
                    writer.WriteStartElement("product");
                    if (soldProduct.Name != null)
                    {
                        writer.WriteAttributeString("name", soldProduct.Name);
                    }

                    writer.WriteAttributeString("price", soldProduct.Price.ToString());

                    // product
                    writer.WriteEndElement();
                }

                // sold-products
                writer.WriteEndElement();

                // user
                writer.WriteEndElement();
            }

            // users
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

        private static void ExportCategoriesWithProductsCountToJSON(string filename, ShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.Products.Count)
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.Products.Count,
                    AveragePrice = c.Products.Average(p => (decimal?)p.Price),
                    TotalRevenue = c.Products.Sum(p => (decimal?)p.Price)
                }).ToList();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            WriteJSONToFile(filename, json);
            Console.WriteLine(json);
        }

        private static void ExportSuccessfullySoldProductsToJSON(string filename, ShopContext context)
        {
            var users = context.Users
                .Where(u => u.Products.Any(p => p.Buyer != null))
                .Include(u => u.Products)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.Products
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price,
                            BuyerFirstName = p.Buyer.FirstName,
                            BuyerLastName = p.Buyer.LastName
                        })
                }).ToList();

            var json = JsonConvert.SerializeObject(users, Formatting.Indented);

            WriteJSONToFile(filename, json);
            Console.WriteLine(json);
        }

        private static void WriteJSONToFile(string filename, string json)
        {
            StreamWriter writer = new StreamWriter(filename);
            writer.Write(json);
            writer.Close();
        }

        static void ExportProductsInRangeToJSON(decimal start, decimal end, string filename, ShopContext context)
        {
            var tem =
                from p in context.Products
                where p.Price > start
                select p.Price;
            var products = context.Products
                .Where(p => (p.Price >= start) && (p.Price <= end) && p.Buyer == null)
                .Include(p => p.Seller.FirstName)
                .Include(p => p.Seller.LastName)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    FullName = p.Seller.FirstName + " " + p.Seller.LastName
                }).ToList();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            WriteJSONToFile(filename, json);
            Console.WriteLine(json);
        }

        private static void ImportJsonCategories(ShopContext context)
        {
            StreamReader reader = new StreamReader("../../categories.json");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(reader.ReadToEnd());
            reader.Close();

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private static void ImportJsonProducts(ShopContext context)
        {  
            StreamReader reader = new StreamReader("../../products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(reader.ReadToEnd());
            reader.Close();

            Random randGenerator = new Random();
            foreach (var product in products)
            {
                if ((randGenerator.Next(1, 100) < 10))
                {
                    product.Buyer = context.Users.Find(randGenerator.Next(1, context.Users.Count() + 1));
                }

                product.Seller = context.Users.Find(randGenerator.Next(1, context.Users.Count() + 1));

                var category = context.Categories.Find(randGenerator.Next(1, context.Categories.Count() + 1));
                product.Categories.Add(category);

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        private static void ImportXmlUsers(ShopContext context)
        {
            XmlDocument inputUsersXml = new XmlDocument();
            inputUsersXml.Load("../../users.xml");
            XmlNodeList xUsers = inputUsersXml.SelectNodes("/users/user");

            foreach (XmlNode xUser in xUsers)
            {
                User user = new User();

                if (xUser.Attributes["first-name"] != null)
                {
                    user.FirstName = xUser.Attributes["first-name"].Value;
                }

                if (xUser.Attributes["last-name"] != null)
                {
                    user.LastName = xUser.Attributes["last-name"].Value;
                }

                if (xUser.Attributes["age"] != null)
                {
                    user.Age = int.Parse(xUser.Attributes["age"].Value);
                }

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
