using MultimediaShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultimediaShop.Models
{
    public class Sale : ISale
    {
        private IItem item;
        private DateTime dateOfPurchase;

        public Sale(IItem item, DateTime saleDate)
        {
            this.Item = item;
            this.DateOfPurchase = saleDate;
        }

        public Sale(IItem item)
            : this(item, DateTime.Today)
        {
        }

        
        public IItem Item
        {
            get { return this.item; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Item cannot be null.");
                }
                this.item = value;
            }
        }

        public DateTime DateOfPurchase { get; set; }

        public override string ToString()
        {
            return string.Format("- {0} {1}\n{2}",
                this.GetType().Name, this.DateOfPurchase, this.Item);
        }
    }
}
