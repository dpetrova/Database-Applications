using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Program
    {
        static void Main()
        {
            //Problem 5.	Transactional ATM Withdrawal
            //Using transactional logic in Entity Framework write a method that withdraws money (for example $200) from given account. 
            //The withdrawal is successful when the following steps are completed successfully:
                //Step 1.	Check if the given CardPIN and CardNumber are valid. Throw an exception if not.
                //Step 2.	Check if the amount on the account (CardCash) is bigger than the requested sum (in our example $200). 
                //Throw an exception if not.
                //Step 3.	The ATM machine pays the required sum (e.g. $200) 
                //and stores in the table CardAccounts the new amount (CardCash = CardCash - 200).
            //Put the above steps in explicit transaction that is started before the first step and is committed after the last step.

            var context = new ATMEntities();
            
            //ATMWithdraw(context, 1);


            //Problem 6.	ATM Transactions History
            //Extend the project from the previous exercise and add a new table TransactionHistory
            //with fields (Id, CardNumber, TransactionDate, Amount) holding information about all money withdrawals on all accounts.
            //Modify the withdrawal logic so that it preserves history in the new table after each successful money withdrawal.

            ATMWithdrawWithHystory(context, 7);

        }

        private static void ATMWithdraw(ATMEntities context, int userId)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                var account = context.CardAccounts.Find(userId);
                try
                {
                    var givenAccount = context.CardAccounts
                        .Where(a => a.CardNumber == account.CardNumber && a.CardPIN == account.CardPIN);
                    foreach (var acc in givenAccount)
                    {
                        Console.Write("Enter card number: ");
                        string card = Console.ReadLine();
                        Console.Write("Enter PIN: ");
                        string pin = Console.ReadLine();
                        if (card != acc.CardNumber || pin != acc.CardPIN)
                        {
                            throw new Exception("Invalid card number or PIN");
                        }
                        Console.Write("Enter sum: ");
                        decimal sum = decimal.Parse(Console.ReadLine());
                        if (acc.CardCash < sum)
                        {
                            throw new Exception("Available cash is not enough to commit transaction");
                        }
                        acc.CardCash -= sum;
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    Console.WriteLine("Transaction has been commited");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }


        private static void ATMWithdrawWithHystory(ATMEntities context, int userId)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                var account = context.CardAccounts.Find(userId);
                try
                {
                    var givenAccount = context.CardAccounts
                        .Where(a => a.CardNumber == account.CardNumber && a.CardPIN == account.CardPIN);
                    foreach (var acc in givenAccount)
                    {
                        Console.Write("Enter card number: ");
                        string card = Console.ReadLine();
                        Console.Write("Enter PIN: ");
                        string pin = Console.ReadLine();
                        if (card != acc.CardNumber || pin != acc.CardPIN)
                        {
                            throw new Exception("Invalid card number or PIN");
                        }
                        Console.Write("Enter sum: ");
                        decimal sum = decimal.Parse(Console.ReadLine());
                        if (acc.CardCash < sum)
                        {
                            throw new Exception("Available cash is not enough to commit transaction");
                        }
                        acc.CardCash -= sum;
                        var note = new TransactionHistory()
                        {
                            CardNumber = card,
                            TransactionDate = DateTime.Now,
                            Amount = sum
                        };
                        context.TransactionHistories.Add(note);
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    Console.WriteLine("Transaction has been commited");
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
