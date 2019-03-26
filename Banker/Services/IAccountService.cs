using Banker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        bool AccountExists(int id);
        Account CreateAccount(Account account);
        bool DeleteAccount(int id);
        Account WithdrawlFromAccount(Account account, Transactions transactions);
        Account DepositIntoAccount(Account account, Transactions transactions);
    }
}
