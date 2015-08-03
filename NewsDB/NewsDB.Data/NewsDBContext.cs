using NewsDB.Models;

namespace NewsDB.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class NewsDBContext : DbContext
    {
        
        public NewsDBContext()
            : base("NewsDBContext")
        {
        }

        public virtual DbSet<News> Newses { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        
    }

    
}