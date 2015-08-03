using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace _12.XMLtoHTML_throughXSL_Stylesheet
{
    class Program
    {
        static void Main()
        {
            //Create an XSL stylesheet, which transforms the file catalog.xml into HTML document,
            //formatted for viewing in a standard Web-browser.
            //Write a C# program to apply the XSLT stylesheet transformation on the file catalog.xml using the class XslTransform.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("../../stylesheet.xslt");
            xslt.Transform("../../../catalog.xml", "../../../catalog.html");
        }
    }
}
