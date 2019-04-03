using System;
using Banker.Repositories;
using Banker.Services;
using Banker.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Banker.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankerTest
{
    public class BankServiceTest
    {
        private static readonly Random random = new Random();
        public ServiceProvider ServiceProvider { get; }

        public BankServiceTest()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IAccountRepository, AccountRepository>()
                .AddSingleton<ITransactionRepository, TransactionRepository>()
                .AddSingleton<IBankService, BankService>()
                .AddDbContext<BankLedgerContext>(opt => opt.UseInMemoryDatabase("Bank Ledger"));
            ServiceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void TestCreateAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            // Create test account and add to the service
            Account account = CreateTestAccount();
            bankService.CreateAccount(account);

            // verify that the account was added.
            Assert.Equal(account, bankService.GetAccount(account.AccountId));
        }

        [Fact]
        public void TestCreateInvalidAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);

            Assert.Null(bankService.CreateAccount(account));
        }

        [Fact]
        public void TestWithdrawFromAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestWithdrawTransaction(account, 100);
            double expectedBalance = account.Balance - transactions.TransactionAmount;
            bankService.WithdrawlFromAccount(account.AccountId, transactions);

            Assert.Equal(account.Balance, expectedBalance);
        }

        [Fact]
        public void TestOverWithdrawFromAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestWithdrawTransaction(account, 100);

            // Something should happen here?
            bankService.WithdrawlFromAccount(account.AccountId, transactions);
        }

        [Fact]
        public void TestDepositIntoAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestDepositTransaction(account, 100);
            double expectedBalance = account.Balance + transactions.TransactionAmount;
            bankService.DepositIntoAccount(account.AccountId, transactions);

            Assert.Equal(account.Balance, expectedBalance);
        }

        private Account CreateTestAccount()
        {
            Account account = new Account()
            {
                AccountId = random.Next(),
                AccountNumber = random.Next(),
                AccountHolder = "Default One",
                SecondaryHolder = "No one",
                Balance = 200.0,
                Password = "password",
                Username = "username"
            };
            return account;
        }

        private Transactions CreateTestWithdrawTransaction(Account account, int amount)
        {
            Transactions transactions = new Transactions()
            {
                TransactionId = 0,
                AccountId = account.AccountId,
                TimeOfTransaction = DateTime.Now,
                TransactionAmount = amount,
                TransactionType = "Withdraw",
                TransactionStatus = "New"
            };
            return transactions;
        }

        private Transactions CreateTestDepositTransaction(Account account, int amount)
        {
            Transactions transactions = new Transactions()
            {
                TransactionId = 0,
                AccountId = account.AccountId,
                TimeOfTransaction = DateTime.Now,
                TransactionAmount = amount,
                TransactionType = "Deposit",
                TransactionStatus = "New"
            };
            return transactions;
        }
    }
}
