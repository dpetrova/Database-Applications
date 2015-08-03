using System.Data.Entity.ModelConfiguration.Conventions;
using ProductsShop.Models;

namespace ProductsShop.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductsShopContext : DbContext
    {
       
        public ProductsShopContext()
            : base("ProductsShopContext")
        {
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //DISABLE CASCADE ON DELETE:

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);


            //Initialize DB relationships betwen Users and Friends
            //Create a table Friends
            modelBuilder.Entity<User>()
                //.HasOptional(u => u.FirstName)
                .HasMany(b => b.Friends)
                .WithMany()
                //.WillCascadeOnDelete(false)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FriendId");
                    m.ToTable("UserFriends");
                });


            //Initialize DB relationships between Products and Users
            modelBuilder.Entity<User>()
                .HasMany(u => u.BougthProducts)
                .WithOptional(p => p.Buyer)
                .Map(m =>
                {
                    m.MapKey("BuyerId");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.SoldProducts)
                .WithRequired(p => p.Seller)
                .Map(m =>
                {
                    m.MapKey("SellerId");
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
   
}