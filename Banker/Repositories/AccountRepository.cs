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
    // CRUD operations for accounts.
    public class AccountRepository : IAccountRepository
    {
        private readonly BankLedgerContext _context;

        public AccountRepository(BankLedgerContext context)
        {
            _context = context;
        }

        // Checking for account, additional.
        public bool AccountExists(int id)
        {
            if(_context.Accounts.Find(id) == null)
            {
                return false;
            }
            return true;
            
        }

        // Create
        public Account CreateAccount(Account account)
        {
            _context.Accounts.Add(account);

            _context.SaveChanges();

            return account;
        }

        // Delete
        public bool DeleteAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            if(account == null)
            {
                return false;
            }
            else
            {
                _context.Accounts.Remove(account);
                return true;
            }
        }

        // Read
        public Account GetAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            return account;
        }

        // Read many
        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        // Update
        public Account UpdateAccount(Account account)
        {
            if (_context.Accounts.Find(account.AccountId) != null)
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();
                return account;
            }
            else
            {
                return null;
            }
        }

        public int? LogIntoAccount(string username, string password)
        {
            Account account = _context.Accounts.Where(x => x.Username.Equals(username)).FirstOrDefault();
            if(account == null)
            {
                return null;
            }
            else if(account.Password != password)
            {
                return null;
            }
            else
            {
                return account.AccountId;
            }
        }
    }
}
