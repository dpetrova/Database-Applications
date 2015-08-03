using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Category
    {
        private static int categoryId = 0;
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.CategoryId = ++categoryId;
        }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
