using Supermarket.Data;
using Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImportFromXML
{
    class ImportFromXML
    {
        static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("../../ExpensesByVendorAndByMonth.xml");
            var context = new SupermarketContext();
            XmlNodeList Vendors = xmlDoc.DocumentElement.SelectNodes("/expenses-by-month/vendor");

            foreach (XmlElement item in Vendors)
            {
                string vendorName = item.Attributes["name"].Value;
                foreach (XmlNode expense in item.ChildNodes)
                {
                    DateTime expenseMonth = DateTime.Parse(expense.Attributes["month"].Value);
                    decimal expensePrice = Convert.ToDecimal(expense.InnerText);
                    try
                    {
                        var vendorId = context.Suppliers.First(v => v.Name == vendorName).Id;
                        context.Expenses.Add(new Expense
                        {
                            Date = expenseMonth,
                            Amount = expensePrice,
                            SupplierId = vendorId
                        });
                        context.SaveChanges();
                    }
                    catch
                    {
                        throw new ArgumentNullException("Such vendor is not found in the database", vendorName);
                    }                                    

                }                
            }
        }
    }
}
