using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Contexts;
using Banker.Models;

namespace Banker.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankLedgerContext _context;

        public AccountRepository(BankLedgerContext context)
        {
            _context = context;
        }

        public bool AccountExists(int id)
        {
            if(_context.Accounts.Find(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Account CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            return account;
        }

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

        public Account GetAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            return account;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public bool UpdateAccount(Account account)
        {
            if (_context.Accounts.Find(account) != null)
            {
                _context.Accounts.Update(account);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
