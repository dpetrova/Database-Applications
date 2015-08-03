using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;
using _01.EntityFrameworkMappings_Database_First;
using Formatting = Newtonsoft.Json.Formatting;

namespace _02.ExportToJSON
{
    class ExportToJSON
    {
        static void Main()
        {
            //Problem 2.	Export the Leagues and Teams as JSON

            //Write a C# application based on your EF data model for exporting all leagues along with their teams.
            //Write the output in a JSON file named leagues-and-teams.json. Include in the output the leagues with no teams (if any).
            //Order the leagues and the teams in each league alphabetically.
            //For better performance, ensure your program executes a single DB query and retrieves from the database only the required data
            //(without unneeded rows and columns).

            var context = new FootballEntities();

            var leagsWithTeams = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    l.LeagueName,
                    Teams = l.Teams
                    .OrderBy(t => t.TeamName)
                    .Select(t => t.TeamName)
                }).ToList();

            //foreach (var league in leagsWithTeams)
            //{
            //    Console.WriteLine("--" + league.LeagueName);
            //    foreach (var team in league.Teams)
            //    {
            //        Console.WriteLine(team);
            //    }
            //}

            //export data to JSON using build-in JSON serializer (have to add reference to System.Web.Extensions):
            //var serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(leagsWithTeams);

            //export data to JSON using JSON.NET
            //in Nuget Package Manager Console type:    Install-Package Newtonsoft.Json
            var json = JsonConvert.SerializeObject(leagsWithTeams, Formatting.Indented);

            //write the output in a JSON file named leagues-and-teams.json in the directory of the project
            File.WriteAllText("../../leagues-and-teams.json", json);
            Console.WriteLine(json);
        }
    }
}
