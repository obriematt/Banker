using Banker.Contexts;
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

        public static void CreatePreconfiguredAccounts(IBankService bankService)
        {
            HelperFunctions.CreateDefaultAccounts(bankService);
        }
    }
}
