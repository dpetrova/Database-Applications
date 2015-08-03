using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using ProductsShop.Data;
using ProductsShop.Data.Migrations;

namespace ProductsShop.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            //set database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsShopContext, Configuration>());

            var context = new ProductsShopContext();
            var count = context.Products.Count();
            Console.WriteLine(count);

            //Problem 3.	Query and Export Data
            //Write the below described queries and export the returned data to the specified format. 
            //Make sure that Entity Framework generates only a single query for each task.

            //Query 1 - Products In Range
            //Get all products in a specified price range (e.g. 500 to 1000) which have no buyer. 
            //Order them by price (from lowest to highest). Select only the product name, price and the full name of the seller. 
            //Export the result to JSON.
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000 && p.Buyer == null)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = p.Seller.FirstName + " " + p.Seller.LastName
                });
            var jsonProductsInRange = JsonConvert.SerializeObject(productsInRange, Newtonsoft.Json.Formatting.Indented);
            var path = "../../../" + "ProductsInRange" + ".json";
            File.WriteAllText(path, jsonProductsInRange);
            Console.WriteLine(jsonProductsInRange);


            //Query 2 - Successfully Sold Products
            //Get all users who have at least 1 sold item with a buyer. Order them by last name, then by first name. 
            //Select the person's first and last name. 
            //For each of the sold products (products with buyers), select the product's name, price and the buyer's first and last name.
            var usersWithSoldProducts = context.Users
                .Where(u => u.SoldProducts.Any())
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    FirstName = u.FirstName ?? "N/A",
                    LastName = u.LastName,
                    SoldProduct = u.SoldProducts.Select(p => new
                    {
                        Productname = p.Name,
                        Price = p.Price,
                        Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                    })
                });
            var jsonUsersWithSoldProducts = JsonConvert.SerializeObject(usersWithSoldProducts, Newtonsoft.Json.Formatting.Indented);
            var path2 = "../../../" + "UsersWithSoldProducts" + ".json";
            File.WriteAllText(path2, jsonUsersWithSoldProducts);
            Console.WriteLine(jsonUsersWithSoldProducts);


            //Query 3 - Categories By Products Count
            //Get all categories. Order them by the number of products in that category (a product can be in many categories).
            //For each category select its name, the number of products, the average price of those products
            //and the total revenue (total price sum) of those products (regardless if they have a buyer or not).
            var categoriesByProductsCount = context.Categories
                .OrderBy(c => c.Products.Count)
                .Select(c => new
                {
                    c.Name,
                    NumberOfProducts = c.Products.Count,
                    AveragePrice = c.Products.Average(p => p.Price),
                    TotalSum = c.Products.Sum(p => p.Price)
                });
            var jsonCategoriesByProductsCount = JsonConvert.SerializeObject(categoriesByProductsCount, Newtonsoft.Json.Formatting.Indented);
            var path3 = "../../../" + "CategoriesByProductsCount" + ".json";
            File.WriteAllText(path2, jsonCategoriesByProductsCount);
            Console.WriteLine(jsonCategoriesByProductsCount);


            //Query 4 - Users and Products
            //Get all users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest),
            //then by last name (ascending). Select only their first and last name, age and for each product - name and price.
            //Export the results to XML. Follow the format below to better understand how to structure your data. 
            //Note: If a user has no first name or age, do not add attributes.
            var usersWithProducts = context.Users
                .Where(u => u.SoldProducts.Count > 0)
                .OrderByDescending(u => u.SoldProducts.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    Count = u.SoldProducts.Count,
                    FirstName = u.FirstName ?? "N/A",
                    LastName = u.LastName,
                    Age = u.Age,
                    Products = u.SoldProducts.Select(p => new
                    {
                        ProductName = p.Name,
                        Price = p.Price
                    })
                });

            var encoding = Encoding.GetEncoding("utf-8");
            using (var writer = new XmlTextWriter("../../../usersWithProducts.xml", encoding))
            {
                writer.Formatting = System.Xml.Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("users");
                foreach (var user in usersWithProducts)
                {
                    writer.WriteStartElement("user");
                    writer.WriteAttributeString("first-name", user.FirstName);
                    writer.WriteAttributeString("last-name", user.LastName);
                    writer.WriteAttributeString("age", user.Age.ToString());
                    writer.WriteStartElement("sold-products");
                    foreach (var product in user.Products)
                    {
                        writer.WriteStartElement("product");
                        writer.WriteAttributeString("name", product.ProductName);
                        writer.WriteAttributeString("price", product.Price.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            Console.WriteLine("Xml document has been created.");
        }
    }
}
