using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _5.ExtractArtistsAndNumberOfAlbums
{
    class Program
    {
        //Write a program that extracts all different artists, which are found in the catalog.xml. 
        //For each artist print the number of albums in the catalogue. 
        //Use the XPath and a Dictionary<string,int> (use the artist name as key and the number of albums as value in the dictionary).
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            var artists = new Dictionary<string, int>();
            string xPathQueryArtists = "/albums/album/artist";
            string xPathQueryAlbums = "/albums/album/name";
            XmlNodeList artistsList = doc.SelectNodes(xPathQueryArtists);
            XmlNodeList albumList = doc.SelectNodes(xPathQueryAlbums);
            foreach (XmlNode artist in artistsList)
            {
                string artistName = artist.InnerText;
                int numOfAlbums = albumList
                    .Cast<XmlNode>()
                    .Where(a => a.NextSibling.InnerText == artistName)
                    .Count();
                if (!artists.ContainsKey(artistName))
                {
                    artists.Add(artistName, numOfAlbums);
                }
            }

            foreach (var artist in artists)
            {
                Console.WriteLine("Artist: {0}; number of albums: {1}", artist.Key, artist.Value);
            }
        }
    }
}
