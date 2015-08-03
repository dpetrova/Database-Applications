using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Supermarket.Data;
using Supermarket.Models;
using Newtonsoft.Json;

namespace Supermarket.Client
{
    class JSONReports
    {
       private const string OutputDir = "../../Json-Reports/";

        //get requested sales by period
        public static IEnumerable<Sale> GetSalesByPeriod(DateTime startDate, DateTime endDate)
        {
            using (var context = new SupermarketContext())
            {
                var salesByPeriod = context.Orders
                    .Where(order => order.Date >= startDate && order.Date <= endDate)
                    .GroupBy(order => new
                    {
                        order.ProductId,
                        ProductName = order.Product.Name,
                        VendorName = order.Product.Supplier.Name,
                        order.Quantity
                    })
                    .Select(sale => new Sale
                    {
                        ProductId = sale.Key.ProductId,
                        ProductName = sale.Key.ProductName,
                        VendorName = sale.Key.VendorName,
                        Quantity = sale.Key.Quantity,
                        Incomes = sale.Sum(prod => prod.Quantity * prod.Product.Price)
                    })
                    .ToList();

                return salesByPeriod;
            }
        }

        //export data in JSON format
        public static void ExportSalesToJson(DateTime startDate, DateTime endDate)
        {
            Directory.CreateDirectory(OutputDir);
            var sales = GetSalesByPeriod(startDate, endDate);
            foreach (var sale in sales)
            {
                var json = JsonConvert.SerializeObject(sale, Newtonsoft.Json.Formatting.Indented);
                var path = OutputDir + sale.ProductId + ".json";
                File.WriteAllText(path, json);
            }
            Console.WriteLine("Data have been exported to JSON.");
        }



    }
}
