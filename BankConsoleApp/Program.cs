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
            StartUp.CreatePreconfiguredAccounts(bankService);
            string username;
            string password;

            // Welcome message.
            DisplayMessaging.WelcomeMessage();
            string createOrNot = Console.ReadLine().ToLower();
            if (createOrNot.Equals("create"))
            {
                Account newAccount = null;
                while (newAccount == null)
                {
                    newAccount = DisplayMessaging.CreateNewAccountFromUser(bankService);
                }
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
                if(account == null)
                {
                    DisplayMessaging.BadUserDisplay();
                }

                // Log in successful, display options.
                while (account != null)
                {
                    string input;
                    int intValue;
                    DisplayMessaging.SelectionOptions();
                    string userInput = Console.ReadLine();
                    convertedInput = (Selections)Enum.Parse(typeof(Selections), userInput);
                    switch (convertedInput)
                    {
                        case Selections.ViewAccount:
                            DisplayMessaging.PrettyDisplayAccount(account);
                            break;

                        case Selections.CheckBalance:
                            DisplayMessaging.DisplayBalance(account);
                            break;

                        case Selections.WithdrawFromAccount:
                            Console.WriteLine("Amount to withdraw from account: ");
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out intValue))
                            {
                                Console.WriteLine("Not a valid number to deposit");
                                Console.WriteLine("Amount to withdraw from account: ");
                                input = Console.ReadLine();
                            }
                            Transactions transactionWithdraw = HelperFunctions.CreateTransaction((int)userKey, intValue, "Withdraw");
                            bankService.WithdrawlFromAccount((int)userKey, transactionWithdraw);
                            break;

                        case Selections.DepositIntoAccount:
                            Console.WriteLine("Amount to deposit into account: ");
                            input = Console.ReadLine();
                            while(!Int32.TryParse(input, out intValue))
                            {
                                Console.WriteLine("Not a valid number to deposit");
                                Console.WriteLine("Amount to deposit into account: ");
                                input = Console.ReadLine();
                            }
                            Transactions transactionDeposit = HelperFunctions.CreateTransaction((int)userKey, intValue, "Deposit");
                            bankService.DepositIntoAccount((int)userKey, transactionDeposit);
                            break;

                        case Selections.SeeTransactionHistory:
                            IEnumerable<Transactions> transactionHistory = bankService.GetAllTransactionsForAccount((int)userKey);
                            DisplayMessaging.PrettyDisplayTransactions(transactionHistory);
                            break;

                        case Selections.CreateAccount:
                            Account accountToCreate = DisplayMessaging.CreateNewAccountFromUser(bankService);
                            Account createdAccount = bankService.CreateAccount(accountToCreate);
                            break;

                        case Selections.LogOut:
                            if (bankService.Logout())
                            {
                                userKey = 0;
                                account = null;
                            }
                            break;
                        default:
                            DisplayMessaging.InvalidInput();
                            break;
                          
                    }
                }
            }
        }
    }
}
