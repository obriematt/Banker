using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Contexts;
using Banker.Models;

namespace Banker.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankLedgerContext _context;

        public TransactionRepository(BankLedgerContext context)
        {
            _context = context;
        }

        public Transactions CreateTransaction(Transactions transaction)
        {
            // Create a new transaction.
            _context.Transactions.Add(transaction);

            // Update the account related to the Transaction.
            var account = _context.Accounts.Find(transaction.AccountId);
            account.transactions.Add(transaction);
            _context.Accounts.Update(account);

            _context.SaveChanges();

            return transaction;
        }

        public bool DeleteTransaction(Account account, int id)
        {
            // Find account related to transaction
            var accountFound = _context.Accounts.Find(account);
            var transaction = _context.Transactions.Find(id);

            // If account or transaction is null return false
            if(accountFound == null || transaction == null)
            {
                return false;
            }
            else
            {
                // Remove transactions
                _context.Transactions.Remove(transaction);

                // Remove transaction history from account
                accountFound.transactions.Remove(transaction);
                _context.Accounts.Update(accountFound);
                return true;
            }
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public IEnumerable<Transactions> GetAllTransactionsForAccount(Account account)
        {
            var accountFound = _context.Accounts.Find(account.AccountId);

            if(accountFound == null)
            {
                return null;
            }
            else
            {
                return accountFound.transactions;
            }
        }

        public Transactions GetTransaction(Account account, int id)
        {
            var accountFound = _context.Accounts.Find(account);
            var transactionFound = _context.Transactions.Find(id);

            if(accountFound == null || transactionFound == null)
            {
                return null;
            }

            return transactionFound;
        }

        public Transactions UpdateTransaction(int id, string transactionStatus)
        {
            var transactionFound = _context.Transactions.Find(id);
            var accountFound = _context.Accounts.Find(transactionFound.AccountId);

            if (accountFound == null || transactionFound == null)
            {
                return null;
            }

            transactionFound.TransactionStatus = transactionStatus;
            accountFound.transactions.Find(x => x.TransactionId.Equals(id)).TransactionStatus = transactionStatus;

            _context.Transactions.Update(transactionFound);
            _context.Accounts.Update(accountFound);

            return transactionFound;
        }
    }
}
