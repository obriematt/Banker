﻿using Banker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Repositories
{
    public interface IAccountRepository
    {
        bool AccountExists(int id);
        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        Account CreateAccount(Account account);
        bool DeleteAccount(int id);
        Account UpdateAccount(Account account);
        int? LogIntoAccount(string username, string password);
    }
}
