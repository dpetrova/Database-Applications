using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _10.XElement_DirectoryContentsAsXML
{
    class Program
    {
        static void Main()
        {
            //Write a program to traverse given directory and write to a XML file its contents together with all subdirectories and files.
            //Use tags <file> and <dir> with attributes. Use XDocument, XElement and XAttribute.
            XElement directoryXml =
                new XElement("root-dir",
                    new XAttribute("path", "C:/Example"),
                    new XElement("dir",
                        new XAttribute("name", "docs"),
                        new XElement("file",
                            new XAttribute("name", "tutorial.pdf")),
                        new XElement("file",
                            new XAttribute("name", "TODO.txt")),
                        new XElement("file",
                            new XAttribute("name", "Presentation.pptx"))),
                    new XElement("dir",
                        new XAttribute("name", "photos"),
                        new XElement("file",
                            new XAttribute("name", "friends.jpg")),
                        new XElement("file",
                            new XAttribute("name", "the_cake.jpg"))
                        )
                    );

            Console.WriteLine(directoryXml);
            directoryXml.Save("../../../directoryXml.xml");
        }
    }
}
