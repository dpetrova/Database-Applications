using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Supermarket.Data;
using Supermarket.Data.Migrations;
using Supermarket.Models;

namespace Supermarket.Client
{
    public class Client
    {
        
        static void Main()
        {
            //set database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketContext, Configuration>());

            var context = new SupermarketContext();

            var count = context.Categories.Count();
            Console.WriteLine(count);


            
            //set period to get sales by it
            DateTime startDate = new DateTime(2015, 07, 15);
            DateTime endDate = new DateTime(2015, 07, 31);

            //export SQL data about sales by given period in JSON format
            JSONReports.ExportSalesToJson(startDate, endDate);

            //import data about requested sales in MongoDB
            //before starting the program you have to connect to mongod:
            //  1.open command prompt and navigate to directory where is file mongod.exe, e.g:
            //  cd C:\Program Files\MongoDB\Server\3.0\bin
            //  2.connect to server, as specify path where is data directory, e.g:
            //  mongod --dbpath ../data
            // 3. set database Supermarket:
            // use Supermarket
            MongoDBImport.MongoDbImportData(startDate, endDate);

        }
    }
}