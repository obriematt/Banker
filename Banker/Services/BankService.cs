using Banker.Models;
using Banker.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Services
{
    public class BankService : IBankService
    {
        // Necessary repos for data access.
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BankService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        // Transaction portion of BankService.

        public Transactions CreateTransaction(Transactions transaction)
        {
            if (!_transactionRepository.TransactionExists(transaction.TransactionId)){
                // The transaction already exists, can't create.
                return null;
            }
            return _transactionRepository.CreateTransaction(transaction);
        }

        public bool DeleteAccount(int id)
        {
            if (!_accountRepository.AccountExists(id))
            {
                // Transaction not found to delete.
                return false;
            }
            else
            {
                // Delete the transaction from records.
                return _accountRepository.DeleteAccount(id);    
            }
        }

        public bool DeleteTransaction(int id)
        {
            if (!_transactionRepository.TransactionExists(id))
            {
                // Transaction not found to delete.
                return false;
            }
            else
            {
                // Delete the transaction from records.
                return _transactionRepository.DeleteTransaction(id);
            }
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }

        public IEnumerable<Transactions> GetAllTransactionsForAccount(int accountId)
        {
            if (!_accountRepository.AccountExists(accountId))
            {
                return null;
            }
            var accountFound = _accountRepository.GetAccount(accountId);
            return accountFound.Transactions;
        }

        public Transactions GetTransaction(int id)
        {
            return _transactionRepository.GetTransaction(id);
        }

        // Account portion of the BankService.

        public Account WithdrawlFromAccount(int accountId, Transactions transaction)
        {
            if (!_accountRepository.AccountExists(accountId))
            {
                // The account doesn't exist. 
                return null;
            }
            else
            {
                var foundAccount = _accountRepository.GetAccount(accountId);

                // Create transaction with status of Pending.
                transaction.TransactionStatus = "Pending";
                _transactionRepository.CreateTransaction(transaction);

                // Check if the account can withdraw the amount, update accordingly
                if(foundAccount.Balance >= transaction.TransactionAmount)
                {
                    foundAccount.Balance = foundAccount.Balance - transaction.TransactionAmount;
                    _accountRepository.UpdateAccount(foundAccount);
                    _transactionRepository.UpdateTransaction(transaction.TransactionId, "Completed");
                }
                else
                {
                    _transactionRepository.UpdateTransaction(transaction.TransactionId, "Failed");
                }

                return foundAccount;
            }
        }

        public Account DepositIntoAccount(int accountId, Transactions transaction)
        {
            if (!_accountRepository.AccountExists(accountId))
            {
                // The account doesn't exist. 
                return null;
            }
            else
            {
                var foundAccount = _accountRepository.GetAccount(accountId);

                // Create transaction with status of Pending.
                transaction.TransactionStatus = "Pending";
                var pending = _transactionRepository.CreateTransaction(transaction);

                // Transaction was created. Update account and transactions.
                if (_transactionRepository.TransactionExists(pending.TransactionId))
                {
                    foundAccount.Balance = foundAccount.Balance + transaction.TransactionAmount;
                    _accountRepository.UpdateAccount(foundAccount);
                    _transactionRepository.UpdateTransaction(pending.TransactionId, "Completed");
                    return foundAccount;
                }

                // Failure case.
                _transactionRepository.UpdateTransaction(transaction.TransactionId, "Failed");
                return null;
            }
        }

        public Account GetAccount(int id)
        {
            if (!_accountRepository.AccountExists(id))
            {
                // The account couldn't be found.
                return null;
            }
            else
            {
                return _accountRepository.GetAccount(id);
            }
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public Account CreateAccount(Account account)
        {
            if (_accountRepository.AccountExists(account.AccountId))
            {
                // The account ID already exists and this is bad.
                return null;
            }
            else if(!ValidUsername(account.Username))
            {
                return null;
            }
            else if (string.IsNullOrEmpty(account.Password))
            {
                return null;
            }
            else
            {
                return _accountRepository.CreateAccount(account);
            }
        }

        public bool AccountExists(int id)
        {
            return _accountRepository.AccountExists(id);
        }

        public Account UpdateAccount(Account account)
        {
            // The account doesn't exist to update.
            if (!_accountRepository.AccountExists(account.AccountId))
            {
                return null;
            }
            return _accountRepository.UpdateAccount(account);
        }

        public int? LogIn(string username, string password)
        {
            return _accountRepository.LogIntoAccount(username, password);
        }

        public bool Logout()
        {
            return true;
        }

        private bool ValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            IEnumerable<Account> accounts = _accountRepository.GetAccounts();
            if (accounts.Any(x => x.Username.Equals(username)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
