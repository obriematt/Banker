using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Models
{
    public class Account
    {
        // Id for the account
        [Key]
        public int AccountId { get; set; }

        // Account number
        public int AccountNumber { get; set; }

        // Primary account holder
        public string AccountHolder { get; set; }

        // Secondary account holder
        public string SecondaryHolder { get; set; }

        // The account balance
        public double Balance { get; set; }

        // History of transactions 
        public List<Transactions> Transactions { get; set; }
    }
}
