using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _4.ExtractArtistsAndNumberOfAlbums
{
    class Program
    {
        static void Main()
        {
            //Write a program that extracts all different artists, which are found in the catalog.xml. 
            //For each artist print the number of albums in the catalogue. 
            //Use the DOM parser and a Dictionary<string,int> (use the artist name as key and the number of albums as value in the dictionary).
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            var artists = new Dictionary<string, int>();
            XmlNode rootNode = doc.DocumentElement;
            foreach (XmlNode albumNode in rootNode.ChildNodes)
            {
                string artist = albumNode["artist"].InnerText;
                int numOfAlbums = rootNode.ChildNodes.Cast<XmlNode>().Count(album => album["artist"].InnerText == artist);
                if (!artists.ContainsKey(artist))
                {
                    artists.Add(artist, numOfAlbums);
                }

                //var artists = albumNode["artist"]
                //    .Cast<XmlNode>()
                //    .GroupBy(a => a.InnerText)
                //    .Select(a => new {Name = a.Key, NumberOfAlbums = a.Count()})
                //    .ToDictionary(a => a.Name, a => a.NumberOfAlbums);
                //foreach (var art in artists)
                //{
                //    Console.WriteLine("Artist: {0}; number of albums: {1}", art.Key, art.Value);
                //}
            }

            foreach (var art in artists)
            {
                Console.WriteLine("Artist: {0}; number of albums: {1}", art.Key, art.Value);
            }
        }
    }
}
