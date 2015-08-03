using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _8.OldAlbums
{
    class Program
    {
        static void Main()
        {
            //Write a program, which extract from the file catalog.xml the titles and prices for all albums, published 5 years ago or earlier.
            //Use XDocument and LINQ to XML query.
            XDocument doc = XDocument.Load("../../../catalog.xml");
            int limitYear = DateTime.Now.Year - 5;

            //var albumsBeforeLimitYear = doc
            //    .Elements("albums").Elements("album")
            //    .Where(a => int.Parse(a.Element("year").Value) < limitYear)
            //    .Select(a => new
            //    {
            //        Title = a.Element("name").Value,
            //        Price = decimal.Parse(a.Element("price").Value)
            //    });

            var albumsBeforeLimitYear =
                from album in doc.Descendants("album")
                where int.Parse(album.Element("year").Value) < limitYear
                select new
                {
                    Title = album.Element("name").Value,
                    Price = album.Element("price").Value
                };
           
            foreach (var album in albumsBeforeLimitYear)
            {
                Console.WriteLine("Album: {0}; price: {1}", album.Title, album.Price);
            }
        }
    }
}
