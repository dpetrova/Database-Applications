using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultimediaShop.Interfaces;
using MultimediaShop.Models;

namespace MultimediaShop.CoreLogic
{
    public static class RentManager
    {
        private static ISet<IRent> rents = new HashSet<IRent>();

        public static IEnumerable<IRent> ReportOverdueRents()
        {
            return rents
                .Where(r => r.RentState == State.Overdue)
                .OrderBy(r => r.CalcRentFine())
                .ThenBy(r => r.Item.Title);
        }

        public static void AddRent(IItem item, DateTime rentDate, DateTime deadline)
        {
            rents.Add(new Rent(item, rentDate, deadline));
        }
    }
}
