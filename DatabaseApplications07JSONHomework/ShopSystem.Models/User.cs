using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ShopSystem.Models
{
    public class User
    {
        private static int userId = 0;
        public User()
        {
            this.Friends = new HashSet<User>();
            this.Products = new HashSet<Product>();
            this.UserId = ++userId;
        }

        
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
