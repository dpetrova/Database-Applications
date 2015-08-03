using BookShopSystem.Data.Migrations;

namespace BookShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using BookShopSystem.Models;

    public class BookShopContext : DbContext
    {
        
        public BookShopContext()
            : base("name=BookShopContext")
        {
        }

        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.RelatedBooks)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("BookId");
                    m.MapRightKey("RelatedBookId");
                    m.ToTable("RelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }
    }

    
}