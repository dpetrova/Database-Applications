using System;
using Supermarket.Data;

namespace ReplicateOracleDBIntoMSSQL
{
    class MainReplicateOracleDb
    {
        static void Main()
        {
            Console.WriteLine("Migrating Oracle data to SQL Server...");
            
            ReplicateMethods.ReplicateOracleDataToSqlServer(new SupermarketContext());
            
            Console.WriteLine("Oracle data replicated.");
        }
    }
}