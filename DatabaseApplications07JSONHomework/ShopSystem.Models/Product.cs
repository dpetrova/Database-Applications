using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Product
    {
        private static int productId = 0;
        public Product()
        {
            this.Categories = new HashSet<Category>();
            this.ProductId = ++productId;
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual User Seller { get; set; }

        public virtual User Buyer { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
