using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Contexts;
using Banker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banker.Repositories
{
    // CRUD operations for transcations
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankLedgerContext _context;

        public TransactionRepository(BankLedgerContext context)
        {
            _context = context;
        }

        // Create
        public Transactions CreateTransaction(Transactions transaction)
        {
            // Create a new transaction.
            _context.Transactions.Add(transaction);

            // Update the account related to the Transaction.
            var account = _context.Accounts.Find(transaction.AccountId);
            account.Transactions.Add(transaction);
            _context.Accounts.Update(account);

            _context.SaveChanges();

            return transaction;
        }

        // Delete 
        public bool DeleteTransaction(int id)
        {
            // Find transaction
            var transaction = _context.Transactions.Find(id);

            // Find related account
            var account = _context.Accounts.Find(transaction.AccountId);

            // If account or transaction is null return false
            if(account == null || transaction == null)
            {
                return false;
            }
            else
            {
                // Remove transactions
                _context.Transactions.Remove(transaction);

                // Remove transaction history from account
                account.Transactions.Remove(transaction);
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return true;
            }
        }

        // Read all
        public IEnumerable<Transactions> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        // Read
        public Transactions GetTransaction(int id)
        {
            var transactionFound = _context.Transactions.Find(id);

            if(transactionFound == null)
            {
                return null;
            }

            return transactionFound;
        }

        public bool TransactionExists(int id)
        {
            if (_context.Transactions.Find(id) == null)
            {
                return false;
            }
            return true;
            
        }

        // Update
        public Transactions UpdateTransaction(int id, string transactionStatus)
        {
            // Find the transaction and account
            var transactionFound = _context.Transactions.Find(id);
            var accountFound = _context.Accounts.Find(transactionFound.AccountId);

            if (accountFound == null || transactionFound == null)
            {
                return null;
            }

            // Update the transactions
            transactionFound.TransactionStatus = transactionStatus;
            accountFound.Transactions.Find(x => x.TransactionId.Equals(id)).TransactionStatus = transactionStatus;

            // Update the lists.
            _context.Transactions.Update(transactionFound);
            _context.Accounts.Update(accountFound);

            _context.SaveChanges();
            return transactionFound;
        }
    }
}
