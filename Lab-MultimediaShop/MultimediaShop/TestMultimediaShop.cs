using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultimediaShop.CoreLogic;
using MultimediaShop.Interfaces;
using MultimediaShop.Models;

namespace MultimediaShop
{
    class Program
    {
        static void Main()
        {
            //Item sallingerBook = new Book("4adwlj4", "Catcher in the Rye", 20.00m, "J. D. Salinger", Genre.SciFi);
            //Item threeManBook = new Book("84djesd", "Three Men in a Boat", 39.99m, "Jerome K. Jerome", new List<Genre> { Genre.Comedy, Genre.History });
            //Item acGame = new Game("9gkjdsa", "AC Revelations", 78.00m, Genre.History, Restriction.Teen);
            //Item bubbleSplashGame = new Game("r8743jf", "Bubble Splash", 7.80m, new List<Genre> { Genre.Child, Genre.Comedy});
            //Item godfatherMovie = new Movie("483252j", "The Godfather", 99.00m, 178, Genre.Criminal);
            //Item dieHardMovie = new Movie("9853kfds", "Die Hard 4", 9.90m, 144, new List<Genre> { Genre.Action, Genre.Criminal, Genre.Triller});

            //DateTime today = DateTime.Now;
            //DateTime fiveYearsAgo = today.AddYears(-5);
            //Sale dieHardSale = new Sale(dieHardMovie, fiveYearsAgo);
            //Console.WriteLine(dieHardSale.DateOfPurchase); // 1/30/2015 2:31:55 PM (today)
            //Sale acSale = new Sale(acGame);
            //Console.WriteLine(acSale.DateOfPurchase); // 1/30/2010 2:31:55 PM


            //DateTime afterOneWeek = today.AddDays(30);
            //Rent bookRent = new Rent(sallingerBook, today, afterOneWeek);
            //Console.WriteLine(bookRent.RentState); // Pending

            //DateTime lastMonth = today.AddDays(-34);
            //DateTime lastWeek = today.AddDays(-8);
            //Rent movieRent = new Rent(godfatherMovie, lastMonth, lastWeek);
            //Console.WriteLine(movieRent.RentState); // Overdue

            //movieRent.ReturnItem();
            //Console.WriteLine(movieRent.RentState); // Returned
            //Console.WriteLine(movieRent.DateOfReturn); // 1/30/2015 2:41:53 PM
            //Console.WriteLine(movieRent.CalcRentFine()); // 7.9200

            
            var storeEngine = new Engine();
            storeEngine.Run();
        }
    }
}
