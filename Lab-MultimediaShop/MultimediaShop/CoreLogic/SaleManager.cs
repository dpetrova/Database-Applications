using System;
using System.Collections.Generic;
using System.Linq;
using MultimediaShop.Interfaces;
using MultimediaShop.Models;

namespace MultimediaShop.CoreLogic
{
    public static class SaleManager
    {
        private static ISet<ISale> sales = new HashSet<ISale>();


        public static decimal ReportSalesIncome(DateTime startDate)
        {
            return sales
                .Where(s => s.DateOfPurchase >= startDate)
                .Sum(s => s.Item.Price);
        }

        public static void AddSale(IItem item, DateTime saleDate)
        {
            sales.Add(new Sale(item, saleDate));
        }
    }
}
