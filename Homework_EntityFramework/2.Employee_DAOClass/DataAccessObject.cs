using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace _2.Employee_DAOClass
{
    public class DataAccessObject
    {
        public static void Add(Employee employee){
           
            var context = new SoftUniEntities();
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine("Employee has been added.");
        }

        public static Employee FindByKey(object key)
        {
            var context = new SoftUniEntities();
            var employee = context.Employees
                .Find(key);
            return employee;
        }

        public static void Modify(int employeeId, string property, object value)
        {
            var context = new SoftUniEntities();
            var selectedEmployee = context.Employees.Find(employeeId);
            selectedEmployee.GetType().GetProperty(property).SetValue(selectedEmployee, value);
            context.SaveChanges();
            Console.WriteLine("Employee has been modified.");
        }

        public static void Delete(int employeeId)
        {
            var context = new SoftUniEntities();
            var selectedEmployee = context.Employees.Find(employeeId);
            context.Employees.Remove(selectedEmployee);
            context.SaveChanges();
            Console.WriteLine("Employee has been deleted.");
        }
    }
}
