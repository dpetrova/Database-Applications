using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using _01.EntityFrameworkMappings_Database_First;

namespace _03.ExportToXML
{
    class ExportToXML
    {
        static void Main()
        {
            //Problem 3.	Export International Matches as XML

            //Write a C# application based on your EF data model for exporting all international matches and their score
            //in a XML file named international-matches.xml.
            //Each match should have home country and away country. Each country should have country code (as attribute). 
            //Use an XML parser by choice.
            //Attach attribute "date-time" when the match has date and time. 
            //Attach attribute "date" when the match has date only (without time). Do not attach any attributes when the match has no date.
            //List the score if the match has score (home goals and away goals). List the league name if the match has a league.
            //Order the matches by date (from the earliest) and by home country and away country alphabetically as second and third criteria.
            //For better performance, ensure your program executes a single DB query and retrieves from the database only the required data
            //(without unneeded rows and columns).

            var context = new FootballEntities();

            var internationlMatches = context.InternationalMatches
                .OrderBy(m => m.MatchDate)
                .ThenBy(m => m.CountryHome.CountryName)
                .ThenBy(m => m.CountryAway.CountryName)
                .Select(m => new
                {
                    date = m.MatchDate,
                    homeCountryName = m.CountryHome.CountryName,
                    awayCountryName = m.CountryAway.CountryName,
                    homeCountryCode = m.HomeCountryCode,
                    awayCountryCode = m.AwayCountryCode,
                    homeGoals = m.HomeGoals,
                    awayGoals = m.AwayGoals,
                    leagueName = m.League.LeagueName
                }).ToList();

            //foreach (var match in internationlMatches)
            //{
            //    Console.WriteLine(match);
            //}

            XElement matches = new XElement("matches");
            foreach (var match in internationlMatches)
            {
                XElement xmlMatch = 
                    new XElement("match",
                        new XElement("home-country",
                            new XAttribute("code", match.homeCountryCode), 
                            match.homeCountryName),
                        new XElement("away-country",
                            new XAttribute("code", match.awayCountryCode),
                            match.awayCountryName)
                    );
                //check if match has league and if has add element
                if (match.leagueName != null)
                {
                    xmlMatch.Add(new XElement("league", match.leagueName));
                }
                //check if match has score and if has add element
                if (match.homeGoals != null && match.awayGoals != null)
                {
                    xmlMatch.Add(new XElement("score", match.homeGoals + "-" + match.awayGoals));
                }
                //check if match has only date or date-time and add corresponding attribute
                if (match.date != null)
                {
                    DateTime dateTime;
                    DateTime.TryParse(match.date.ToString(), out dateTime);
                    if (dateTime.TimeOfDay.TotalSeconds == 0)
                    {
                        xmlMatch.Add(new XAttribute("date", dateTime.ToString("dd-MMM-yyyy")));
                    }
                    else
                    {
                        xmlMatch.Add(new XAttribute("date-time", dateTime.ToString("dd-MMM-yyyy hh:mm")));
                    }
                }
                matches.Add(xmlMatch);
            }
            Console.WriteLine(matches);
            //export data to .xml file
            matches.Save("../../international-matches.xml");
        }
    }
}
