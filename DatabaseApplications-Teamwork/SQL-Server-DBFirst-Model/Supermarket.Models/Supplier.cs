using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Supplier
    {
        private ICollection<Product> products;

        public Supplier()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}