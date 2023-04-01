using System;

namespace MySuperBank // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount("Krishn",10000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance}.");
            account.MakeWithdrawal(120, DateTime.Now, "GTA-V");
            Console.WriteLine(account.Balance);

            // Test that the initial balances must be positive.
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                //Console.WriteLine(e.ToString());
                
            }
            // Test for a negative balance.
            try
            {
                account.MakeWithdrawal(75000, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                //Console.WriteLine(e.ToString());
            }
            account.MakeWithdrawal(50, DateTime.Now, "PS Game");
            Console.WriteLine(account.Balance);
            Console.WriteLine(account.GetAccountHistory());
            account.MakeDeposit(10000, DateTime.Now, "Salary credited");
            Console.WriteLine(account.GetAccountHistory());
        }
    }
}