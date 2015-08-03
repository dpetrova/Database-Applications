using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;

namespace Homework_EntityFrameworkPerformance
{
    class Program
    {
        static void Main()
        {
            var context = new AdsEntities();
            //Problem 1.Show Data from Related Tables

            //Using Entity Framework write a SQL query to select all ads from the database and later print
            //their title, status, category, town and user. Do not use Include(…) for the relationships of the Ads. 
            var adsWithoutInclude = context.Ads;
            foreach (var ad in adsWithoutInclude)
            {
                var title = ad.Title;
                var status = ad.AdStatus.Status;
                var category = ad.Category != null ? ad.Category.Name : "no category"; //за тези колони (пропъртита), които е позволено да са NULL, трябва да се направи проверка иначе хвърля ексепшън
                var town = ad.Town != null ? ad.Town.Name : "no town";
                var user = ad.AspNetUser.UserName;
                Console.WriteLine("{0}; {1}; {2}; {3}; {4}", 
                                    title, status, category, town, user);
            }
            Console.WriteLine();

            //Add Include(…) to select statuses, categories, towns and users along with all ads.
            var adsWithInclude = context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AspNetUser);
            foreach (var ad in adsWithInclude)
            {
                var title = ad.Title;
                var status = ad.AdStatus.Status;
                var category = ad.Category != null ? ad.Category.Name : "no category"; //за тези пропъртита, които е позволено да са NULL, трябва да се направи проверка иначе хвърля ексепшън
                var town = ad.Town != null ? ad.Town.Name : "no town";
                var user = ad.AspNetUser.UserName;
                Console.WriteLine("{0}; {1}; {2}; {3}; {4}",
                                    title, status, category, town, user);
            }
            Console.WriteLine();


            //Problem 2. Play with ToList()

            //Using Entity Framework select all ads from the database, then invoke ToList(), 
            //then filter the categories whose status is Published; then select the ad title, category and town, 
            //then invoke ToList() again and finally order the ads by publish date.
            //Rewrite the same query in a more optimized way
            //Compare the execution time of the two programs. Hint: use the System.Diagnostics.Stopwatch class. 
            //Run each program 10 times and write the average performance time
            
            Stopwatch stopwatch = new Stopwatch(); // Create new stopwatch
            for (int i = 0; i < 10; i++)
            {
                stopwatch.Start();  // Begin timing
                IncorrectUseToList(context);    // Do something
                stopwatch.Stop();   // Stop timing
                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);  // Write result
            }
            Console.WriteLine();
            stopwatch.Restart();

            for (int i = 0; i < 10; i++)
            {
                stopwatch.Start();  // Begin timing
                CorrectUseToList(context);    // Do something
                stopwatch.Stop();   // Stop timing
                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);  // Write result
            }


            //Problem 3. Select Everything vs. Select Certain Columns

            //Write a program to compare the execution speed between these two scenarios:
            //Select everything from the Ads table and print only the ad title.
            //Select the ad title from Ads table and print it.

            Stopwatch stopwatch2 = new Stopwatch(); // Create new stopwatch
            for (int i = 0; i < 10; i++)
            {
                stopwatch2.Start();  // Begin timing
                SelectEverything(context);    // Do something
                stopwatch2.Stop();   // Stop timing
                Console.WriteLine("Time elapsed: {0}", stopwatch2.Elapsed);  // Write result
            }
            Console.WriteLine();
            stopwatch2.Restart();

            for (int i = 0; i < 10; i++)
            {
                stopwatch2.Start();  // Begin timing
               SelectCertainColumns(context);    // Do something
                stopwatch2.Stop();   // Stop timing
                Console.WriteLine("Time elapsed: {0}", stopwatch2.Elapsed);  // Write result
            }

        }

        private static void SelectCertainColumns(AdsEntities context)
        {
            var ads = context.Ads
                .Select(a => a.Title);
            foreach (var ad in ads)
            {
                Console.WriteLine(ad);
            }
        }

        private static void SelectEverything(AdsEntities context)
        {
            context.Database.SqlQuery<string>("CHECKPOINT");
            context.Database.SqlQuery<string>("DBCC DROPCLEANBUFFERS"); //clean SQL Server caches 
            var ads = context.Ads;
            foreach (var ad in ads)
            {
                Console.WriteLine(ad.Title);
            }
        }

        private static void CorrectUseToList(AdsEntities context)
        {
            context.Database.SqlQuery<string>("CHECKPOINT");
            context.Database.SqlQuery<string>("DBCC DROPCLEANBUFFERS"); //clean SQL Server caches 
            var correctToList = context.Ads
                .OrderBy(a => a.Date)
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    a.Title,
                    Category = a.Category != null ? a.Category.Name : "no category",
                    Town = a.Town != null ? a.Town.Name : "no town"
                })
                .ToList();
        }

        private static void IncorrectUseToList(AdsEntities context)
        {
            context.Database.SqlQuery<string>("CHECKPOINT");
            context.Database.SqlQuery<string>("DBCC DROPCLEANBUFFERS"); //clean SQL Server caches 
            var ads = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    a.Title,
                    Category = a.Category != null ? a.Category.Name : "no category",
                    Town = a.Town != null ? a.Town.Name : "no town",
                    a.Date
                })
                .ToList()
                .OrderBy(a => a.Date);
        }
    }
}
