using Banker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Contexts
{
    public class BankLedgerContext : DbContext
    {
        public BankLedgerContext(DbContextOptions<BankLedgerContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transactions> Transactions { get; set; }
    }
}

