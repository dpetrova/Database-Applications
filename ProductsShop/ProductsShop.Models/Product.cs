using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsShop.Models
{
    public class Product
    {
        private ICollection<Category> categories;
       
        public Product()
        {
            this.categories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        //[InverseProperty("SoldProducts")]
        //public int SellerId { get; set; }
        public virtual User Seller { get; set; }

        //[InverseProperty("BougthProducts")]
        //public int BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        
    }
}
