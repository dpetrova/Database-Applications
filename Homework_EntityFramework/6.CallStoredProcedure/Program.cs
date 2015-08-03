using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace _6.CallStoredProcedure
{
    class Program
    {
        static void Main()
        {
            var context = new SoftUniEntities();

            //call the stored procedure (see it in SoftUniEntities.Context.cs)
            //to find all projects for given employee by his/her firstname and lastname
            var projects = context.GetProjectsByEmployee("Ruth", "Ellerbrock");
            foreach (var project in projects)
            {
                Console.WriteLine("{0} - {1} - {2}", project.Name, project.Description, project.StartDate);
            }
        }
    }
}
