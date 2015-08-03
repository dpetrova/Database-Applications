using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace DatabaseSearchQueries
{
    class Program
    {
        static void Main()
        {
            var context = new SoftUniEntities();

            //1.Find all employees who have projects started in the time period 2001 - 2003 (inclusive).
            //Select the project's name, start date, end date and manager name.
            var employees = context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Projects = e.Projects.Select(p => new
                    {
                        ProjectName = p.Name,
                        p.StartDate,
                        p.EndDate
                    }).ToList(),
                    ManagerName = e.Manager.FirstName + " " + e.Manager.LastName
                });

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.FirstName + " " + employee.LastName + ": ");
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine("Project name: {0} / Start date: {1} / End date: {2} / Manager: {3}",
                            project.ProjectName, project.StartDate.ToString("dd-MM-yyyy"), project.EndDate.ToString(), employee.ManagerName);
                }
                Console.WriteLine();
            }


            //2. Find all addresses, ordered by the number of employees who live there (descending), then by town name (ascending).
            //Take only the first 10 addresses and select their address text, town name and employee count.
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    NumberOfEmployes = a.Employees.Count
                })
                .ToList();

            foreach (var address in addresses)
            {
                Console.WriteLine("{0}, {1} - {2} employees",
                        address.AddressText, address.TownName, address.NumberOfEmployes);
            }


            //3.Get an employee by id (e.g. 147). Select only his/her first name, last name, job title and projects (only their names).
            //The projects should be ordered by name (ascending).
            var emps = context.Employees
                .Where(e => e.EmployeeID == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.Projects
                        .Select(p => new
                        {
                            p.Name
                        })
                        .OrderBy(p => p.Name)
                        .ToList()
                });

            foreach (var emp in emps)
            {
                Console.WriteLine("First name: {0}; Last name: {1}; Job title: {2}", 
                                                    emp.FirstName, emp.LastName, emp.JobTitle);
                foreach (var project in emp.Projects)
                {
                    Console.WriteLine("Project: {0}", project.Name);
                }
            }


            //4.Find all departments with more than 5 employees. Order them by employee count (ascending).
            //Select the department name, manager name and first name, last name, hire date and job title of every employee.
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .Select(d => new
                {
                    Department = d.Name,
                    Manager = d.Manager,
                    Employees = d.Employees
                        .Select(e => new
                        {
                            e.FirstName,
                            e.LastName,
                            e.HireDate,
                            e.JobTitle
                        }).ToList(),
                });

            foreach (var department in departments)
            {
                Console.WriteLine("--{0} - Manager: {1}, Employees:",
                    department.Department,
                    department.Manager.LastName);
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine(employee.FirstName + " " + employee.LastName +
                        "; Hire date: " + employee.HireDate.ToString("dd-MM-yyyy") + "; Job title: " + employee.JobTitle);
                }
            }

        }
    }
}
