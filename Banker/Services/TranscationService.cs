using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Models;
using Banker.Repositories;

namespace Banker.Services
{
    public class TranscationService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TranscationService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Transactions CreateTransaction(Transactions transaction)
        {

            return _transactionRepository.CreateTransaction(transaction);
        }

        public bool DeleteTransaction(Account account, int id)
        {
            return _transactionRepository.DeleteTransaction(account, id);
        }

        public IEnumerable<Transactions> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }

        public IEnumerable<Transactions> GetAllTransactionsForAccount(Account account)
        {
            return GetAllTransactionsForAccount(account);
        }

        public Transactions GetTransaction(Account account, int id)
        {
            return GetTransaction(account, id);
        }
    }
}
