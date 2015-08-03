using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using _01.EntityFrameworkMappings_Database_First;

namespace _04.ImportFromXML
{
    class ImportFromXML
    {
        static void Main()
        {
            //Problem 4.	Import Leagues and Teams from XML

            //Write a C# application based on your EF data model for importing leagues and teams. 
            //The application should process multiple requests and write logs for each operation at the console.
            //The input comes from an XML file leagues-and-teams.xml 
            //The input XML holds a sequence of requests given in the <league>…</league> elements.
            //The element "league-name" is optional. The specified league should be created in the database, if it does not exist.
            //The elements "teams" and "team" are optional. The specified teams should be created in the database, if they do not exist.
            //Note that team "name" is mandatory, but team "country" is optional.
            //A team is considered existing in the database when it is matched by both name and country.
            //If both elements "league-name" and "teams" exist in a certain query, all teams, not in the given league, should be added to it.
            //The output should be printed on the console.
            //Your program should correctly parse the input XML.
            //Your program should correctly import leagues (new and existing).
            //Your program should correctly import teams (new and existing).
            //Your program should correctly add teams to leagues (when a team does not already belong to a league).

            var context = new FootballEntities();

            //parsing XML document
            XmlDocument doc = new XmlDocument();
            doc.Load("../../leagues-and-teams.xml");

            var root = doc.DocumentElement; //it is <leagues-and-teams>
            int id = 1;

            foreach (XmlNode xmlLeague in root.ChildNodes) //root.ChildNodes are <league>
            {
                Console.WriteLine("Processing league #{0} ...", id++);
                XmlNode leagueNameNode = xmlLeague.SelectSingleNode("league-name");

                League league = null;
                if (leagueNameNode != null)
                {
                    string leagueName = leagueNameNode.InnerText; //get league name
                    league = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);
                    if (league != null) //check if database has already this league
                    {
                        Console.WriteLine("Existing league: {0}", leagueName); //if league exists in database; it is same as: context.Leagues.Any(l => l.LeagueName == leagueName)
                    }
                    else
                    {
                        league = new League()
                        {
                            LeagueName = leagueName
                        };
                        context.Leagues.Add(league); //if league doesn't exist add it to database
                        Console.WriteLine("Created league: {0}", leagueName);
                    }
                }

                XmlNode teamsNode = xmlLeague.SelectSingleNode("teams"); //get element <teams>
                if (teamsNode != null) //check if tere is element <teams>
                {
                    foreach (XmlNode xmlTeam in teamsNode.ChildNodes) //tese are elements <team>
                    {
                        Team team = null;
                        string teamName = xmlTeam.Attributes["name"].Value; //get team name from attribute "name" (it is mandatory)
                        string countryName = null; //get country name from attribute "country" (it is optional)
                        if (xmlTeam.Attributes["country"] != null)
                        {
                           countryName = xmlTeam.Attributes["country"].Value;
                        }

                        team =
                            context.Teams.FirstOrDefault(
                                t => t.TeamName == teamName && t.Country.CountryName == countryName); //it will return Team or null
                        if (team != null) //check if database has already this team
                        {
                            Console.WriteLine("Existing team: {0} ({1})", teamName, countryName ?? "no country"); //if team exists in database
                        }
                        else
                        {
                            Country country = context.Countries.FirstOrDefault(c => c.CountryName == countryName); //it will returns null or Country
                            team = new Team()
                            {
                                TeamName = teamName,
                                Country = country
                            };

                            context.Teams.Add(team); //if league doesn't exist add it to database
                            Console.WriteLine("Created team: {0} ({1})", teamName, countryName ?? "no country");
                        }
                        if (league != null)
                        {
                            if (league.Teams.Contains(team))
                            {
                                Console.WriteLine("Existing team in league: {0} belongs to {1}", team.TeamName, league.LeagueName);
                            }
                            else
                            {
                                league.Teams.Add(team);
                                Console.WriteLine("Added team to league: {0} to league {1}", team.TeamName, league.LeagueName);
                            }
                            

                        }
                    }
                }


                
            }

            

            context.SaveChanges();
        }
    }
}
