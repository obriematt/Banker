using Banker.Models;
using Microsoft.AspNetCore.Mvc;
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
        Account CreateAccount(Account account);
        bool DeleteAccount(int id);
        Account WithdrawlFromAccount(int accountId, Transactions transactions);
        Account DepositIntoAccount(int accountId, Transactions transactions);
        string LogIn(string username, string password);
        bool logout(string username);


        // All the transaction services
        IEnumerable<Transactions> GetAllTransactions();
        IEnumerable<Transactions> GetAllTransactionsForAccount(int accountId);
        Transactions GetTransaction(int id);
        Transactions CreateTransaction(Transactions transaction);
        bool DeleteTransaction(int id);
    }
}
