using Banker.Models;
using Banker.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConsoleApp
{
    class HelperFunctions
    {
        private static readonly Random random = new Random();

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

        internal static void PrettyDisplayTransactions(IEnumerable<Transactions> transactions)
        {
            if (transactions != null)
            {
                foreach (Transactions transactionsToShow in transactions)
                {
                    Console.WriteLine("The type of the transaction: " + transactionsToShow.TransactionType);
                    Console.WriteLine("The status of the transaction: " + transactionsToShow.TransactionStatus);
                    Console.WriteLine("The amount of the transaction: " + transactionsToShow.TransactionAmount);
                    Console.WriteLine("The time of the transaction: " + transactionsToShow.TimeOfTransaction);
                }
            }
            else
            {
                Console.WriteLine("No transactions to show for this account");
            }
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

        internal static void DisplayBalance(Account account)
        {
            Console.WriteLine("The account balance for " + account.AccountHolder + " is: " + account.Balance);
        }

        internal static Transactions CreateTransaction(int accountId, int amount, string typeOfTransaction)
        {
            Transactions transactions = new Transactions()
            {
                TransactionId = 0,
                AccountId = accountId,
                TimeOfTransaction = DateTime.Now,
                TransactionAmount = amount,
                TransactionType = typeOfTransaction,
                TransactionStatus = "New"
            };
            return transactions;
        }

        internal static Account CreateAccountObject(string username, string password, string accountHolder, string secondaryHolder = null) 
        {

            int accountNumber = random.Next();
            int accountId = random.Next();
            Account account = new Account()
            {
                AccountId = accountId,
                AccountNumber = accountNumber,
                AccountHolder = accountHolder,
                SecondaryHolder = secondaryHolder,
                Balance = 0.0,
                Username = username,
                Password = password
            };
            return account;
        }

        internal static void CreateDefaultAccounts(IBankService bankService)
        {
            Account firstDefault = new Account()
            {
                AccountId = random.Next(),
                AccountNumber = random.Next(),
                AccountHolder = "Default One",
                SecondaryHolder = "No one",
                Balance = 200.0,
                Password = "password",
                Username = "username"
            };

            Account secondDefault = new Account()
            {
                AccountId = random.Next(),
                AccountNumber = random.Next(),
                AccountHolder = "Default two",
                SecondaryHolder = "No one",
                Balance = 100.0,
                Username = "usernametwo",
                Password = "passwordtwo"
            };
            bankService.CreateAccount(firstDefault);
            bankService.CreateAccount(secondDefault);
        }
    }
}
