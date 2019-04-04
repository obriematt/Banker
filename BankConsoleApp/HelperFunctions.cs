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

        internal static Transactions CreateTransaction(int accountId, int amount, string typeOfTransaction)
        {
            Transactions transactions = new Transactions()
            {
                TransactionId = random.Next(),
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
                Username = username.ToLower(),
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
