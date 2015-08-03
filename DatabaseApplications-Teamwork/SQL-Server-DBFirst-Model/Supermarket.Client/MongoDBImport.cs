using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Supermarket.Models;

namespace Supermarket.Client
{
     public class MongoDBImport
    {
        private const string DatabaseHost = "mongodb://127.0.0.1";
        private const string DatabaseName = "Supermarket";


         //connect to a database
        public static MongoDatabase GetDatabase(string databaseName, string databaseHost)
        {
            MongoClient mongoClient = new MongoClient(databaseHost);
            MongoServer server = mongoClient.GetServer();
            return server.GetDatabase(databaseName);
        }


        //save requested data in MongoDB
        public static void MongoDbImportData(DateTime startDate, DateTime endDate)
        {
            var mongoDatabase = GetDatabase(DatabaseName, DatabaseHost);
            var sales = mongoDatabase.GetCollection<Sale>("SalesByProductReports");
            var salesByPeriod = JSONReports.GetSalesByPeriod(startDate, endDate);
            sales.InsertBatch(salesByPeriod);
            Console.WriteLine("Data have been saved in MongoDB.");
        }
    }
}
