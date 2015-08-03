using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _05.CodeFirst_Movies.Migrations;

namespace _05.CodeFirst_Movies
{
    class CodeFirst
    {
        static void Main()
        {
            //set database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesEntities, Configuration>());
            var context = new MoviesEntities();
            var moviesCount = context.Movies.Count();
        }
    }
}
