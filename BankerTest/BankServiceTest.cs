using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using FakeItEasy;
using Banker.Repositories;
using Banker.Services;
using Banker.Models;

namespace BankerTest
{
    [TestFixture]
    class BankServiceTest : IDisposable
    {
        IUnityContainer _unityContainer;
        private static readonly Random random = new Random();

        [OneTimeSetUp]
        public void SetUp()
        {
            _unityContainer = new UnityContainer();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            Dispose();
        }

        public void Dispose()
        {
            if(_unityContainer != null)
            {
                _unityContainer.Dispose();
                _unityContainer = null;
            }
        }

        [Test]
        public void TestCreateAccount()
        {
            IUnityContainer child = _unityContainer.CreateChildContainer();
            IAccountRepository accountRespository = child.Resolve<IAccountRepository>();
            ITransactionRepository transactionRepository = child.Resolve<ITransactionRepository>();
            IBankService bankService = child.Resolve<BankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);

            Assert.AreEqual(account, bankService.GetAccount(account.AccountId));
        }

        [Test]
        public void TestCreateInvalidAccount()
        {
            IUnityContainer child = _unityContainer.CreateChildContainer();
            IAccountRepository accountRespository = child.Resolve<IAccountRepository>();
            ITransactionRepository transactionRepository = child.Resolve<ITransactionRepository>();
            IBankService bankService = child.Resolve<BankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);

            Assert.IsNull(bankService.CreateAccount(account));
        }

        [Test]
        public void TestWithdrawFromAccount()
        {
            IUnityContainer child = _unityContainer.CreateChildContainer();
            IAccountRepository accountRespository = child.Resolve<IAccountRepository>();
            ITransactionRepository transactionRepository = child.Resolve<ITransactionRepository>();
            IBankService bankService = child.Resolve<BankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestWithdrawTransaction(account, 100);
            double expectedBalance = account.Balance - transactions.TransactionAmount;
            bankService.WithdrawlFromAccount(account.AccountId, transactions);

            Assert.AreEqual(account.Balance, expectedBalance);
        }

        [Test]
        public void TestOverWithdrawFromAccount()
        {
            IUnityContainer child = _unityContainer.CreateChildContainer();
            IAccountRepository accountRespository = child.Resolve<IAccountRepository>();
            ITransactionRepository transactionRepository = child.Resolve<ITransactionRepository>();
            IBankService bankService = child.Resolve<BankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestWithdrawTransaction(account, 100);

            // Something should happen here?
            bankService.WithdrawlFromAccount(account.AccountId, transactions);
        }

        [Test]
        public void TestDepositIntoAccount()
        {
            IUnityContainer child = _unityContainer.CreateChildContainer();
            IAccountRepository accountRespository = child.Resolve<IAccountRepository>();
            ITransactionRepository transactionRepository = child.Resolve<ITransactionRepository>();
            IBankService bankService = child.Resolve<BankService>();

            Account account = CreateTestAccount();
            bankService.CreateAccount(account);
            Transactions transactions = CreateTestDepositTransaction(account, 100);
            double expectedBalance = account.Balance + transactions.TransactionAmount;
            bankService.DepositIntoAccount(account.AccountId, transactions);

            Assert.AreEqual(account.Balance, expectedBalance);
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
