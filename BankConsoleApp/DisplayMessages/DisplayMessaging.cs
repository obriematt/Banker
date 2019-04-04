using Banker.Models;
using Banker.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConsoleApp
{
    class DisplayMessaging
    {
        internal static void PrettyDisplayAccount(Account account)
        {
            Console.WriteLine();
            Console.WriteLine($"The account number is : {account.AccountNumber}");
            Console.WriteLine($"The primary holder of the account is : {account.AccountHolder}");
            if (account.SecondaryHolder != null)
            {
                Console.WriteLine($"The secondary holder of the account is : {account.SecondaryHolder}");
            }
            Console.WriteLine($"The account balance is : {account.Balance}");
            Console.WriteLine();
        }

        internal static void PrettyDisplayTransactions(IEnumerable<Transactions> transactions)
        {
            if (transactions != null)
            {
                foreach (Transactions transactionsToShow in transactions)
                {
                    Console.WriteLine();
                    Console.WriteLine($"The type of the transaction: {transactionsToShow.TransactionType}");
                    Console.WriteLine($"The status of the transaction: {transactionsToShow.TransactionStatus}");
                    Console.WriteLine($"The amount of the transaction: {transactionsToShow.TransactionAmount}");
                    Console.WriteLine($"The time of the transaction: {transactionsToShow.TimeOfTransaction}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No transactions to show for this account");
                Console.WriteLine();
            }
        }

        internal static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the world's great bankledger application.");
            Console.WriteLine("Please log into your account");
            Console.WriteLine("Please create an account to start or use one of the two default accounts.");
            Console.WriteLine("Default one.");
            Console.WriteLine("Username : username.");
            Console.WriteLine("Password : password.");
            Console.WriteLine("Default two.");
            Console.WriteLine("Username : usernametwo.");
            Console.WriteLine("Password : passwordtwo.");
            Console.WriteLine("Please type create to make a new account or press any key to skip");
        }

        internal static void SelectionOptions()
        {
            Console.WriteLine("Please make a selection of the following options:");
            Console.WriteLine("Press 1 to view your account");
            Console.WriteLine("Press 2 to view your balance");
            Console.WriteLine("Press 3 to Withdraw from your account");
            Console.WriteLine("Press 4 to Deposit into your account");
            Console.WriteLine("Press 5 to see your transaction history");
            Console.WriteLine("Press 6 to create a new account");
            Console.WriteLine("Press 7 to logout");
        }

        internal static void DisplayBalance(Account account)
        {
            Console.WriteLine();
            Console.WriteLine($"The account balance for {account.AccountHolder} is {account.Balance}");
            Console.WriteLine();
        }

        internal static void InvalidInput()
        {
            Console.WriteLine();
            Console.WriteLine("Invalid input please select from the following.");
            Console.WriteLine();
        }

        internal static void BadUserDisplay()
        {
            Console.WriteLine();
            Console.WriteLine("The user credentials were incorrect");
            Console.WriteLine();
        }

        internal static Account CreateNewAccountFromUser(IBankService bankService)
        {
            string username = null;
            string password = null;
            Account newAccount = null;


            Console.WriteLine("Enter a username: ");
            username = Console.ReadLine();
            Console.WriteLine("Enter a password: ");
            password = Console.ReadLine();
            Console.WriteLine("Enter the primary account holder : ");
            string accountHolder = Console.ReadLine();
            Account account = HelperFunctions.CreateAccountObject(username, password, accountHolder);
            newAccount = bankService.CreateAccount(account);
            if (newAccount == null)
            {
                Console.WriteLine("Invalid account, please try again.");
            }
            return newAccount;
        }
    }
}
