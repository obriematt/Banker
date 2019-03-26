using Banker.Models;
using Banker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Services
{
    public class BankService : ITransactionService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BankService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public bool AccountExists(int id)
        {
            return _accountRepository.AccountExists(id);
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepository.CreateAccount(account);
        }

        public Transactions CreateTransaction(Transactions transaction)
        {
            return _transactionRepository.CreateTransaction(transaction);
        }

        public bool DeleteAccount(int id)
        {
            return _accountRepository.DeleteAccount(id);
        }

        public bool DeleteTransaction(Account account, int id)
        {
            return _transactionRepository.DeleteTransaction(account, id);
        }

        public Account GetAccount(int id)
        {
            return _accountRepository.GetAccount(id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }

        public IEnumerable<Transactions> GetAllTransactionsForAccount(Account account)
        {
            return _transactionRepository.GetAllTransactionsForAccount(account);
        }

        public Transactions GetTransaction(Account account, int id)
        {
            return _transactionRepository.GetTransaction(account, id);
        }

        public Account WithdrawlFromAccount(Account account, Transactions transactions)
        {
            return null;
        }

        public Account DepositIntoAccount(Account account, Transactions transactions)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTransaction(Account account, int id)
        {
            throw new NotImplementedException();
        }

        public Account UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
