using System.Data.Entity.ModelConfiguration.Conventions;
using _05.CodeFirst_Movies.Migrations;

namespace _05.CodeFirst_Movies
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MoviesEntities : DbContext
    {
        public MoviesEntities()
            : base("name=MoviesEntities")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesEntities, Configuration>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Rating> Ratings { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //DISABLE CASCADE ON DELETE:

           // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            

            base.OnModelCreating(modelBuilder);
        }

    }

    
}