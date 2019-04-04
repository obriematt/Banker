using System;
using Banker.Repositories;
using Banker.Services;
using Banker.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Banker.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);

            // verify that the account was added.
            Assert.Equal(account, bankService.GetAccount(account.AccountId));
        }

        [Fact]
        public void TestCreateInvalidAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);

            Assert.Null(bankService.CreateAccount(account));
        }

        [Fact]
        public void TestWithdrawFromAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount("username", "password");
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
            string expected = "Failed";
            double originalBalance = 200.0;

            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestWithdrawTransaction(account, 1000);

            bankService.WithdrawlFromAccount(account.AccountId, transactions);
            Transactions failedTransaction = bankService.GetTransaction(transactions.TransactionId);
            

            Assert.Equal(failedTransaction.TransactionStatus, expected);
            Assert.Equal(account.Balance, originalBalance);
        }

        [Fact]
        public void TestDepositIntoAccount()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();

            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestDepositTransaction(account, 100);
            double expectedBalance = account.Balance + transactions.TransactionAmount;
            bankService.DepositIntoAccount(account.AccountId, transactions);

            Assert.Equal(account.Balance, expectedBalance);
        }

        [Fact]
        public void TestGetAllTransactionsForBank()
        {
            int expected = 2;
            IBankService bankService = ServiceProvider.GetService<IBankService>();
            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);
            Account account2 = CreateTestAccount("usernameone", "passwordtwo");

            Transactions transactionsOne = CreateTestDepositTransaction(account, 100);
            Transactions transactionsTwo = CreateTestWithdrawTransaction(account2, 5);
            bankService.CreateTransaction(transactionsOne);
            bankService.CreateTransaction(transactionsTwo);

            List<Transactions> transactionList = bankService.GetAllTransactions().ToList();

            Assert.Equal(expected, transactionList.Count);
        }

        [Fact]
        public void TestGetAllTransactionsForAccount()
        {
            int expected = 1;
            IBankService bankService = ServiceProvider.GetService<IBankService>();
            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);
            Account account2 = CreateTestAccount("usernameone", "passwordtwo");

            Transactions transactionsOne = CreateTestDepositTransaction(account, 100);
            Transactions transactionsTwo = CreateTestDepositTransaction(account2, 100);
            bankService.CreateTransaction(transactionsOne);
            bankService.CreateTransaction(transactionsTwo);

            List<Transactions> transactionList = bankService.GetAllTransactionsForAccount(account.AccountId).ToList();

            Assert.Equal(expected, transactionList.Count);
        }

        [Fact]
        public void GetAccountById()
        {
            IBankService bankService = ServiceProvider.GetService<IBankService>();
            Account account = CreateTestAccount("username", "password");
            bankService.CreateAccount(account);
            Account account2 = CreateTestAccount("usernameone", "passwordtwo");
            bankService.CreateAccount(account2);

            Account actualAccountOne = bankService.GetAccount(account.AccountId);
            Account actualAccounTwo = bankService.GetAccount(account2.AccountId);

            Assert.Equal(account, actualAccountOne);
            Assert.Equal(account2, actualAccounTwo);
        }

        private Account CreateTestAccount(string username, string password)
        {
            Account account = new Account()
            {
                AccountId = random.Next(),
                AccountNumber = random.Next(),
                AccountHolder = "Default One",
                SecondaryHolder = "No one",
                Balance = 200.0,
                Password = password,
                Username = username
            };
            return account;
        }

        private Transactions CreateTestWithdrawTransaction(Account account, int amount)
        {
            Transactions transactions = new Transactions()
            {
                TransactionId = 01,
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
                TransactionId = 02,
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
