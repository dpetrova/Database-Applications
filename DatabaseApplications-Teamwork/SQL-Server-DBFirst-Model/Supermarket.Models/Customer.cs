using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models
{
    public class Customer
    {
        private ICollection<Order> orders;

        public Customer()
        {
            this.orders = new HashSet<Order>();
        }
            
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}