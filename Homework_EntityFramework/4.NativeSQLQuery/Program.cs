using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace _4.NativeSQLQuery
{
    class Program
    {
        static void Main()
        {
            //Measure the difference in performance in both cases with a Stopwatch. 
            var context = new SoftUniEntities();
            //Establish connection to server in advance
            var totalCount = context.Employees.Count();

            var sw = new Stopwatch();
            sw.Start();
            PintNamesWithNativeQuery();
            Console.WriteLine("Native: {0}", sw.Elapsed);

            sw.Restart();
            PintNamesWithLinqQuery();
            Console.WriteLine("Linq: {0}", sw.Elapsed);

            //result: Native: 00:00:00.0503817; Linq: 00:00:00.0081855

        }

        //Find all employees who have projects with start date in the year 2002. Select only their first name.

        //native SQL query:
        public static void PintNamesWithNativeQuery()
        {
            var context = new SoftUniEntities();
            string query = @"SELECT e.[FirstName]
                           FROM [SoftUni].[dbo].[Employees] e
                           JOIN EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID
                           JOIN Projects p ON ep.ProjectID = p.ProjectID
                           WHERE DATEPART(yy, p.StartDate) = 2002";
            var employees = context.Database.SqlQuery<string>(String.Format(query)).ToList();
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee);
            //}
        }

        //LINQ query:
        public static void PintNamesWithLinqQuery()
        {
            var context = new SoftUniEntities();
            var employees = context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                .Select(e => e.FirstName);
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee);
            //}
        }
    }
}
