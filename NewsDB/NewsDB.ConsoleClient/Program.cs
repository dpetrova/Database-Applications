using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsDB.Data;
using NewsDB.Data.Migrations;

namespace NewsDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDBContext, Configuration>());
            
            var context = new NewsDBContext();

            //create database
            var count = context.Newses.Count();
            Console.WriteLine(count);


            //Problem 2. Concurrent Updates
            //Write a console app that edits the first news item in the DB. It should detect and handle any concurrent updates.
                //Step 1.   At startup, the app should load from the DB the news text and print it on the console.
                //Step 2.	After that, the app should enter a new value for the news text from the console.
                //Step 3.	After entering a new value, the app should try to save it to the DB.
                    //o	In case of success (no conflicting updates), the app should say that the changes were saved and should finish its work.
                    //o	In case of concurrent update conflict, the app should display an error message, should display the new (changed) text from the DB and should go to Step 2.
            //Run simultaneously two instances of your app to simulate two concurrent users. Make an update conflict in the database and handle it.

            //OptimisticConcurrencyLastWins();

            ConcurrencyFirstWins();
           
        }

        private static void OptimisticConcurrencyLastWins()
        {
            // The first user changes some record
            var contextFirstUser = new NewsDBContext();
            var newsFirstUser = contextFirstUser.Newses.Find(1);
            Console.WriteLine("Text from DB: " + newsFirstUser.Content);
            Console.Write("Enter the corrected text: ");
            string newValueFirstUser = Console.ReadLine();
            newsFirstUser.Content = newValueFirstUser;

            // The second user changes the same record
            var contextSecondUser = new NewsDBContext();
            var newsSecondUser = contextSecondUser.Newses.Find(1);
            Console.WriteLine("Text from DB: " + newsSecondUser.Content);
            Console.Write("Enter the corrected text: ");
            string newValueSecondUser = Console.ReadLine();
            newsSecondUser.Content = newValueSecondUser;

            // Conflicting changes: last wins
            contextFirstUser.SaveChanges();
            contextSecondUser.SaveChanges();
            Console.WriteLine("Changes successfully saved in the DB.");
        }

        private static void ConcurrencyFirstWins()
        {
            // The first user changes some record
            var contextFirstUser = new NewsDBContext();
            var newsFirstUser = contextFirstUser.Newses.Find(1);
            Console.WriteLine("Text from DB: " + newsFirstUser.Content);
            Console.Write("Enter the corrected text: ");
            string newValueFirstUser = Console.ReadLine();
            newsFirstUser.Content = newValueFirstUser;

            // The second user changes the same record
            var contextSecondUser = new NewsDBContext();
            var newsSecondUser = contextSecondUser.Newses.Find(1);
            Console.WriteLine("Text from DB: " + newsSecondUser.Content);
            Console.Write("Enter the corrected text: ");
            string newValueSecondUser = Console.ReadLine();
            newsSecondUser.Content = newValueSecondUser;

            // Conflicting changes: first wins; second gets an exception
            contextFirstUser.SaveChanges();
            try
            {
                contextSecondUser.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Conflict! Text from DB: " + newValueFirstUser);
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
