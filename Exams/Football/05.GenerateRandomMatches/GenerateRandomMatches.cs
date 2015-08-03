using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using _01.EntityFrameworkMappings_Database_First;

namespace _05.GenerateRandomMatches
{
    class GenerateRandomMatches
    {
        //Problem 5.	* Generate Random Matches

        //Write a C# application based on your EF data model for generating random matches with random score. 
        //The application should process multiple requests and write logs for each operation at the console.
        //The input comes from an XML file generate-matches.xml
        //All elements and attributes in the input XML are non-mandatory.
            //•	When "generate-count" is not specified, it has default value of 10.
            //•	When "max-goals" is not specified, it has default value of 5.
            //•	When "start-date" is not specified, it has default value of "1-Jan-2000".
            //•	When "end-date" is not specified, it has default value of "31-Dec-2015".
            //The output should be printed at the console 
        //Your program should correctly parse the input XML.
        //Your program should correctly generate random matches.
        //Your program should correctly save in the database the generated random matches.
        //Your program should correctly print to the console the generated random matches.
        
        public static Random rand = new Random();
        public static FootballEntities context = new FootballEntities();

        static void Main()
        {
            int processedMatchId = 0;
            var xmlDoc = XDocument.Load(@"..\..\generate-matches.xml");
            
        }
    }
}
