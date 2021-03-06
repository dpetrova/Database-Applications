﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace _2.Employee_DAOClass
{
    class Program
    {
        static void Main()
        {
            Employee employee = new Employee();
            employee.FirstName = "Dimitar";
            employee.LastName = "Penchev";
            employee.JobTitle = "Tool Designer";
            employee.DepartmentID = 2;
            employee.HireDate = DateTime.Now;
            employee.Salary = 20000;

            //1.Insert an employee
            DataAccessObject.Add(employee);

            //2.Prints his/her primary key generated by the DB
            int employeeId = employee.EmployeeID;
            Console.WriteLine(employeeId);

            //3.Changes the employee first name and saves it to the database
            DataAccessObject.Modify(employeeId, "FirstName", "Marin");

            //4.Deletes an employee
            DataAccessObject.Delete(employeeId);

        }
    }
}
