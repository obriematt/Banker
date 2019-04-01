using System;
using Banker;
using Banker.Services;
using Banker.Models;
using Banker.Repositories;
using Banker.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BankConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start up.
            IBankService bankService = StartUp.GetConfiguredServices();
            HelperFunctions.WelcomeMessage();
            Selections convertedInput;
            while (true)
            {
                HelperFunctions.SelectionOptions();
                string userInput = Console.ReadLine();
                convertedInput = (Selections)Enum.Parse(typeof(Selections), userInput);
                switch (convertedInput)
                {
                    case Selections.ViewAccount:
                        Console.WriteLine("This is for a view account");
                        break;
                    case Selections.CheckBalance:
                        break;
                    case Selections.WithdrawFromAccount:
                        break;
                    case Selections.DepositIntoAccount:
                        break;
                    case Selections.SeeTransactionHistory:
                        break;
                    case Selections.LogOut:
                        break;
                }

            }
            StartUp.CreateDefaultAccounts(bankService);
            Account newAccount = new Account()
            {
                AccountId = 10101,
                AccountNumber = 101,
                AccountHolder = "Megan",
                SecondaryHolder = null,
                Balance = 1000000
            };

            bankService.CreateAccount(newAccount);
            var account = bankService.GetAccount(01);
            HelperFunctions.PrettyDisplayAccount(account);
            Console.ReadLine();
            
        }
    }
}
