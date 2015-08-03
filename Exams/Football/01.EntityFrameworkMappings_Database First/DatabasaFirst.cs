using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.EntityFrameworkMappings_Database_First
{
    class DatabasaFirst
    {
        static void Main()
        {
            //Problem 1.	Entity Framework Mappings (Database First)
            
            //Create an Entity Framework (EF) data model of the existing database (map the database tables to C# classes). 
            //Use the "database first" model in EF. To test your EF data model, list all team names.
            //Make sure all navigation properties have good (self-describing) names.

            var context = new FootballEntities();

            //list all team names
            var teamsNames = context.Teams.Select(t => t.TeamName);
            foreach (var team in teamsNames)
            {
                Console.WriteLine(team);
            }

            //като се правят задаяите в отделни проекти: всеки проект трябва да има референция към този, където е връзката с базата;
            //на всеки проект да се сложи Entity Framework от package manager-a;
            //и да се копира connection string от App.config на този проект и да се сложи в App.config на всички други проекти
        }
    }
}
