using Banker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transactions> GetAllTransactions();
        IEnumerable<Transactions> GetAllTransactionsForAccount(Account account);
        Transactions GetTransaction(Account accoount, int id);
        Transactions CreateTransaction(Transactions transaction);
        bool DeleteTransaction(Account account, int id);
    }
}
