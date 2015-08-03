using System.Linq;
using Supermarket.Data;
using Supermarket.Models;

namespace ReplicateOracleDBIntoMSSQL
{
    public class ReplicateMethods
    {
        public static void ReplicateOracleDataToSqlServer(SupermarketContext context)
        {
            ReplicateCategories(context);
            ReplicateCustomers(context);
            ReplicateMeasures(context);
            ReplicateSuppliers(context);
            ReplicateProducts(context);
            ReplicateOrders(context);
        }

        private static void ReplicateCategories(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var categories = oracleDb.CATEGORIES.ToList();
            foreach (var category in categories)
            {
                if (context.Categories.Any(c => c.Name == category.NAME) == false)
                {
                    context.Categories.Add(new Category
                    {
                        Name = category.NAME,
                        Description = category.DESCRIPTION
                    });
                }
            }
            context.SaveChanges();
        }
        private static void ReplicateCustomers(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var customers = oracleDb.CUSTOMERS.ToList();
            foreach (var customer in customers)
            {
                if (context.Customers.Any(c => c.Name == customer.NAME) == false)
                {
                    context.Customers.Add(new Customer
                    {
                        Name = customer.NAME,
                        Address = customer.ADDRESS,
                        Phone = customer.PHONE
                    });
                }
            }
            context.SaveChanges();
        }
        private static void ReplicateMeasures(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var measures = oracleDb.MEASURES.ToList();
            foreach (var measure in measures)
            {
                if (context.Measures.Any(m => m.Name == measure.NAME) == false)
                {
                    context.Measures.Add(new Measure
                    {
                        Name = measure.NAME
                    });
                }
            }
            context.SaveChanges();
        }
        private static void ReplicateSuppliers(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var suppliers = oracleDb.SUPPLIERS.ToList();
            foreach (var supplier in suppliers)
            {
                if (context.Suppliers.Any(s => s.Name == supplier.NAME) == false)
                {
                    context.Suppliers.Add(new Supplier
                    {
                        Name = supplier.NAME,
                        Address = supplier.ADDRESS,
                        Phone = supplier.PHONE
                    });
                }
            }
            context.SaveChanges();
        }
        private static void ReplicateProducts(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var products = oracleDb.PRODUCTS.ToList();
            foreach (var product in products)
            {
                if (context.Products.Any(p => p.Name == product.NAME) == false)
                {
                    context.Products.Add(new Product
                    {
                        Name = product.NAME,
                        Price = product.PRICE,
                        SupplierId = (int)product.SUPPLIER_ID,
                        CategoryId = (int)product.CATEGORY_ID,
                        MeasureId = (int)product.MEASURE_ID
                    });
                }
            }
            context.SaveChanges();
        }
        private static void ReplicateOrders(SupermarketContext context)
        {
            var oracleDb = new OracleEntities();

            var orders = oracleDb.ORDERS.ToList();
            foreach (var order in orders)
            {
                if (context.Orders.Any(o => o.Id == order.ID) == false)
                {
                    context.Orders.Add(new Order
                    {
                        Quantity = order.QUANTITY,
                        ProductId = (int)order.PRODUCT_ID,
                        Discount = order.DISCOUNT,
                        Date = order.ORDER_DATE,
                        CustomerId = (int)order.CUSTOMER_ID
                    });
                }
            }
            context.SaveChanges();
        }
    }
}