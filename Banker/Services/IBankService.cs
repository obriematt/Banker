using Banker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Services
{
    public interface IBankService
    {
        // All the account services
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        bool AccountExists(int id);
        Account CreateAccount(Account account);
        bool DeleteAccount(int id);
        Account WithdrawFromAccount(Account account, Transactions transaction);
        Account DepositIntoAccount(Account account, Transactions transaction);

        // All the transaction services
        IEnumerable<Transactions> GetTransactions();
        IEnumerable<Transactions> GetTransactionsForAccount(Account account);
        Transactions GetTransaction(Account account);
        Transactions CreateTransaction(Transactions transaction);
        bool DeleteTransaction(Account account, int id);
        
    }
}
