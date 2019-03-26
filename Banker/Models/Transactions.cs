using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Models
{
    public class Transactions
    {
        // Unique transaction identifier
        [Key]
        public int TransactionId { get; set; }

        // Account related to the transaction
        public int AccountId { get; set; }

        // Time of transaction
        public DateTime TimeOfTransaction { get; set; }

        // Amount of transaction
        public double TransactionAmount { get; set; }

        // Transaction Type
        public string TransactionType { get; set; }

        // Transaction Status
        public string TransactionStatus { get; set; }
    }
}
