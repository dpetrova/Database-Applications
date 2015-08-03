using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models
{
    public class Sale
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Incomes { get; set; }
    }
}
