using System;
using System.Text;
using MultimediaShop.Interfaces;

namespace MultimediaShop.Models
{
    public class Rent : IRent
    {
        private IItem item;
        private State rentState;
        private DateTime dateOfRent;
        private DateTime deadline;
        private DateTime dateOfReturn;

        public Rent(IItem item, DateTime rentDate, DateTime deadline)
        {
            this.Item = item;
            this.dateOfRent = rentDate;
            this.Deadline = deadline;
        }

        public Rent(IItem item, DateTime rentDate)
            : this(item, rentDate, rentDate.AddDays(30))
        {            
        }

        public Rent(IItem item)
            : this(item, DateTime.Now, DateTime.Now.AddDays(30))
        {
        }
        
    
        public IItem Item
        {
            get { return this.item; }
            private set
            {
                if (value == null)
                {
                    throw  new ArgumentNullException("Item cannot be null");
                }
                this.item = value;
            }
        }

        public State RentState
        {
            get
            {
                var now = DateTime.Now;

                if (IsSetDate(this.DateOfReturn))
                {
                    return State.Returned;
                }
                else if (now > this.Deadline)
                {
                    return State.Overdue;
                }
                else
                {
                    return State.Pending;
                }
            }
        }
        
        public DateTime DateOfRent { get; private set; }

        public DateTime Deadline { get; private set; }

        public DateTime DateOfReturn { get; private set; }
        
        

        public decimal CalcRentFine()
        {
            int overdueDays = (DateTime.Today.Date - this.Deadline.Date).Days;
            decimal fine = (decimal)0.01 * Item.Price * overdueDays;
            return Math.Max(fine, 0);
        }

        private bool IsSetDate(DateTime dateTime)
        {
            return dateTime.Year > 1;
        }

        public void ReturnItem()
        {
            this.dateOfReturn = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(string.Format("- {0} {1}", this.GetType().Name, this.RentState));
            result.AppendLine(this.Item.ToString());
            result.AppendLine(string.Format("Rent fine: {0:F2}", CalcRentFine()));

            return result.ToString();
        }
    }
}
