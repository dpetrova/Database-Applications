using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _3.ExtractAllArtistsAlphabetically
{
    class Program
    {
        static void Main()
        {
            //Write a program that extracts all artists in alphabetical order from catalog.xml. Use the DOM parser. 
            //Keep the artists in a SortedSet<string> to avoid duplicates and to keep the artist in alphabetical order.
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            var sortedArtists = new SortedSet<string>();
            XmlNode rootNode = doc.DocumentElement;
            foreach (XmlNode album in rootNode)
            {
                sortedArtists.Add(album["artist"].InnerText);
            }

            foreach (var artist in sortedArtists)
            {
                Console.WriteLine(artist);
            }
        }
    }
}
