using Banker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repositories
{
    public interface ITransactionRepository
    {
        bool TransactionExists(int id);
        IEnumerable<Transactions> GetAllTransactions();
        Transactions GetTransaction(int id);
        Transactions CreateTransaction(Transactions transaction);
        bool DeleteTransaction(int id);
        Transactions UpdateTransaction(int id, string transcationStatus);
    }
}
