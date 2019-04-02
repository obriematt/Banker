using System;
using Banker;
using Banker.Services;
using Banker.Models;
using Banker.Repositories;
using Banker.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BankConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start up and create defaults.
            IBankService bankService = StartUp.GetConfiguredServices();
            HelperFunctions.CreateDefaultAccounts(bankService);
            string username;
            string password;

            // Welcome message.
            HelperFunctions.WelcomeMessage();
            Console.WriteLine("Please create an account to start or use one of the two default accounts.");
            Console.WriteLine("Please type create to make a new account or press any key to skip");
            string createOrNot = Console.ReadLine().ToLower();
            if (createOrNot.Equals("create"))
            {
                Console.WriteLine("Enter a username: ");
                username = Console.ReadLine();
                Console.WriteLine("Enter a password: ");
                password = Console.ReadLine();
                Console.WriteLine("Enter the primary account holder : ");
                string accountHolder = Console.ReadLine();
                Account account = HelperFunctions.CreateAccountObject(username, password, accountHolder);
            }

            // User selection and interactions.
            Selections convertedInput;
            while (true)
            {
                // User must log into an account first.
                Console.WriteLine("Please log into an account");
                Console.WriteLine("Enter username: ");
                username = Console.ReadLine();

                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();

                int? userKey = bankService.LogIn(username, password);
                Account account = null;

                if (userKey != null)
                {
                    account = bankService.GetAccount((int)userKey);
                }
                
                // Log in successful, display options.
                while (account != null)
                {
                    HelperFunctions.SelectionOptions();
                    string userInput = Console.ReadLine();
                    convertedInput = (Selections)Enum.Parse(typeof(Selections), userInput);
                    switch (convertedInput)
                    {
                        case Selections.ViewAccount:
                            HelperFunctions.PrettyDisplayAccount(account);
                            break;
                        case Selections.CheckBalance:
                            HelperFunctions.DisplayBalance(account);
                            break;
                        case Selections.WithdrawFromAccount:
                            Console.WriteLine("Amount to withdraw from account: ");
                            int amountWithdraw = Convert.ToInt32(Console.ReadLine());
                            Transactions transactionWithdraw = HelperFunctions.CreateTransaction((int)userKey, amountWithdraw, "Withdraw");
                            bankService.WithdrawlFromAccount((int)userKey, transactionWithdraw);
                            break;

                        case Selections.DepositIntoAccount:
                            Console.WriteLine("Amount to deposit into account: ");
                            int amountDeposit = Convert.ToInt32(Console.ReadLine());
                            Transactions transactionDeposit = HelperFunctions.CreateTransaction((int)userKey, amountDeposit, "Deposit");
                            bankService.DepositIntoAccount((int)userKey, transactionDeposit);
                            break;
                        case Selections.SeeTransactionHistory:
                            IEnumerable<Transactions> transactionHistory = bankService.GetAllTransactionsForAccount((int)userKey);
                            HelperFunctions.PrettyDisplayTransactions(transactionHistory);
                            break;
                        case Selections.LogOut:

                            // Log out to access separate account.
                            if (bankService.logout())
                            {
                                userKey = 0;
                                account = null;
                            }
                            break;
                    }
                }
            }
        }
    }
}
