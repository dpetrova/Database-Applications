namespace ShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ShopSystem.Models;

    public class ShopContext : DbContext
    {        
        public ShopContext()
            : base("name=ShopContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }

   
}