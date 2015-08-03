using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Discount { get; set; }
        public DateTime? Date { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}