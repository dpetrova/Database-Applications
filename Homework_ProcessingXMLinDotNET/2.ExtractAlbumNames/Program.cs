using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2.ExtractAlbumNames
{
    class Program
    {
        static void Main()
        {
            //Write a program that extracts all album names from catalog.xml. Use the DOM parser.

            XmlDocument doc = new XmlDocument();
            doc.Load("../../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                Console.WriteLine("Album name: {0}", node["name"].InnerText);
            }
        }
    }
}
