using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_EntityFramework;

namespace _5.ConcurrentDatabaseChangesWithEF
{
    class Program
    {
        static void Main()
        {
            var contextOne = new SoftUniEntities();
            var empOne = contextOne.Employees.Find(1);
            empOne.FirstName = "Mincho";

            var contextTwo = new SoftUniEntities();
            var empTwo = contextTwo.Employees.Find(1);
            empTwo.FirstName = "Zdravko";
            
            contextOne.SaveChanges();
            contextTwo.SaveChanges();

            //result with [Concurrency Mode] = None: second win: the first name is set to Zdravko

            //result with [Concurrency Mode] = Fixed: first win: the first name is set to Mincho
        }
    }
}
