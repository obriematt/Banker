using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Contexts;
using Banker.Models;
using Banker.Repositories;

namespace Banker.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionService _transactionService;

        public AccountService(IAccountRepository accountRepository, ITransactionService transactionService)
        {
            _accountRepository = accountRepository;
            _transactionService = transactionService;
        }

        public bool AccountExists(int id)
        {
            return _accountRepository.AccountExists(id);
        }

        public Account CreateAccount(Account account)
        {
            return _accountRepository.CreateAccount(account);
        }

        public bool DeleteAccount(int id)
        {
            return _accountRepository.DeleteAccount(id);
        }

        public Account DepositIntoAccount(Account account, Transactions transactions)
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int id)
        {
            return _accountRepository.GetAccount(id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public Account WithdrawlFromAccount(Account account, Transactions transactions)
        {
            throw new NotImplementedException();
        }
    }
}
