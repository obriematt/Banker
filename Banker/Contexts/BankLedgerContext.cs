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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasAlternateKey(c => c.Username);
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transactions> Transactions { get; set; }
    }
}

