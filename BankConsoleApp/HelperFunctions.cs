using Banker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConsoleApp
{
    class HelperFunctions
    {
        internal static void PrettyDisplayAccount(Account account)
        {
            Console.WriteLine("The account number is :" + account.AccountNumber);
            Console.WriteLine("The primary holder of the account is : " + account.AccountHolder);
            if(account.SecondaryHolder != null)
            {
                Console.WriteLine("The secondary holder of the account is : " + account.SecondaryHolder);
            }
            Console.WriteLine("The account balance is : " + account.Balance);
        }

        internal static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the world's great bankledger application.");
            Console.WriteLine("Please log into your account");
        }

        internal static void SelectionOptions()
        {
            Console.WriteLine("Please make a selection of the following options:");
            Console.WriteLine("Press 1 to view your account");
            Console.WriteLine("Press 2 to view your balance");
            Console.WriteLine("Press 3 to Withdraw from your account");
            Console.WriteLine("Press 4 to Deposit into your account");
            Console.WriteLine("Press 5 to see your transaction history");
            Console.WriteLine("Press 6 to logout");
        }
    }
}
