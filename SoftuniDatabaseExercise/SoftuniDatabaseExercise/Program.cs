using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SoftuniDatabaseExercise
{
    public class DatabaseTestMain
    {
        public static void Main()
        {
            var context = new SoftUniEntities();

            //Employees with Salary Over 50 000
            var employeeNames = context.Employees
                .Where(emp => emp.Salary > 50000)
                .Select(emp => emp.FirstName);
            foreach (var employee in employeeNames)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();

            //Employees from "Research and Development" department
            var emps = context.Employees
                .Where(emp => emp.Department.Name == "Research and Development")
                .OrderBy(emp => emp.Salary)
                .ThenByDescending(emp => emp.FirstName)
                .Select(emp => new
                {
                    //Anonymous object
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    DepartmentName = emp.Department.Name,
                    Salary = emp.Salary
                });
            foreach (var emp in emps)
            {
                Console.WriteLine("{0} {1} from {2} - ${3:F2}",
                    emp.FirstName,
                    emp.LastName,
                    emp.DepartmentName,
                    emp.Salary);
            }
            Console.WriteLine();

            //Adding a New Address and Updating Employee
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };
            context.Addresses.Add(address);
            context.SaveChanges();
            var nakov = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");
            nakov.Address = address;
            context.SaveChanges();
            Console.WriteLine(nakov.Address.AddressText);
            Console.WriteLine();

            //Deleting Project by Id
            //The project is referenced by the junction (many-to-many) table EmployeesProjects.
            //Therefore we cannot safely delete it. First, we need to remove any references to that row in the Projects table.
            var project = context.Projects.Find(2);
            var employeesWithProject = project.Employees.ToList();
            foreach (var employee in employeesWithProject)
            {
                employee.Projects.Remove(project);
            }
            context.Projects.Remove(project);
            context.SaveChanges();

        }
    }
}
