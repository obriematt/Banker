﻿using Banker.Contexts;
using Banker.Models;
using Banker.Repositories;
using Banker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankConsoleApp
{
    class StartUp
    {
        public static IBankService GetConfiguredServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<BankLedgerContext>(opt => opt.UseInMemoryDatabase("Bank Ledger"))
                .AddSingleton<IAccountRepository, AccountRepository>()
                .AddSingleton<ITransactionRepository, TransactionRepository>()
                .AddSingleton<IBankService, BankService>()
                .BuildServiceProvider();

            var bankerService = serviceProvider.GetService<IBankService>();
            return bankerService;
        }

        public static void CreateDefaultAccounts(IBankService bankService)
        {
            // Default account one
            Account firstAccount = new Account()
            {
                AccountId = 01,
                AccountNumber = 007,
                AccountHolder = "Megan",
                SecondaryHolder = null,
                Balance = 1000000
            };
            bankService.CreateAccount(firstAccount);

            Account secondAccount = new Account()
            {
                AccountId = 02,
                AccountNumber = 006,
                AccountHolder = "Matthew",
                SecondaryHolder = null,
                Balance = 10
            };
            bankService.CreateAccount(secondAccount);
        }
    }
}
