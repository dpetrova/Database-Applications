using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _7.OldAlbums
{
    class Program
    {
        static void Main()
        {
            //Write a program, which extract from the file catalog.xml the titles and prices for all albums, published 5 years ago or earlier.
            //Use XPath query.
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            int limitYear = DateTime.Now.Year - 5;

            //string xPathQuery = "/albums/album";
            //XmlNodeList albums = doc.SelectNodes(xPathQuery);
            //foreach (XmlNode album in albums)
            //{
            //    var year = int.Parse(album["year"].InnerText);
            //    if (year <= limitYear)
            //    {
            //        var price = double.Parse(album["price"].InnerText);
            //        var title = album["name"].InnerText;
            //        Console.WriteLine("Album: {0}; price: {1}", title, price);
            //    }
            //}

            string xPathQuery =
                "/albums/album[year <= " + limitYear + "]";

            XmlNodeList albumsList = doc.SelectNodes(xPathQuery);

            foreach (XmlNode album in albumsList)
            {
                Console.WriteLine("Album: {0}, Price: {1}",
                    album["name"].InnerText, album["price"].InnerText);
            }
        }
    }
}
