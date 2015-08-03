using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _6.DeleteAlbums
{
    class Program
    {
        static void Main()
        {
            //Using the DOM parser write a program to delete from catalog.xml all albums having price > 20. 
            //Save the result in a new file cheap-albums-catalog.xml.
            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode album in rootNode.ChildNodes)
            {
                if (double.Parse(album["price"].InnerText) > 20)
                {
                    rootNode.RemoveChild(album);
                }
            }
            Console.WriteLine("All albums more expensive than 20.00 have been deleted.");
            doc.Save("../../../cheaperCatalog.xml");
        }
    }
}
