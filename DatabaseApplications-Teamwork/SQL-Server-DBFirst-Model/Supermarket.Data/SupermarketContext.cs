

using Supermarket.Models;

namespace Supermarket.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Supermarket.Data.Migrations;

    public class SupermarketContext : DbContext
    {
        public SupermarketContext()
            : base("SupermarketModel")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Measure> Measures { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<Expense> Expenses { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}