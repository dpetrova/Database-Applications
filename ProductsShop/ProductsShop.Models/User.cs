using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Models
{
    public class User
    {
        private ICollection<Product> soldProducts;
        private ICollection<Product> bougthProducts;
        private ICollection<User> friends;

        public User()
        {
            this.soldProducts = new HashSet<Product>();
            this.bougthProducts = new HashSet<Product>();
            this.friends =new HashSet<User>();
        }
        
        //[Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        //[Required]
        //[MinLength(3)]
        public string LastName { get; set; }
        
        public int Age { get; set; }

        //[InverseProperty("SellerId")]
        public virtual ICollection<Product> SoldProducts
        {
            get { return this.soldProducts; }
            set { this.soldProducts = value; }
        }

        //[InverseProperty("BuyerId")]
        public virtual ICollection<Product> BougthProducts
        {
            get { return this.bougthProducts; }
            set { this.bougthProducts = value; }
        }

        public virtual ICollection<User> Friends
        {
            get { return this.friends; }
            set { this.friends = value; }
        }
    }
}
