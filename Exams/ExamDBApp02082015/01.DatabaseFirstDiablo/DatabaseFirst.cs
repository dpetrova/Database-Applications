using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.DatabaseFirstDiablo
{
    class DatabaseFirst
    {
        
        static void Main()
        {
            var context = new DiabloEntities();

            //list all character names
            var names = context.Characters.Select(c => c.Name);
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
