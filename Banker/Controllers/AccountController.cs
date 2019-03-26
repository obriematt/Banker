using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banker.Contexts;
using Banker.Models;
using Banker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Banker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankLedgerContext _context;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public AccountController(BankLedgerContext context, ITransactionService transactionService, IAccountService accountService)
        {
            _context = context;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Login()
        {
            return null;
        }

        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> ViewTransactionHistory()
        {
            return null;
        }

        [HttpPost]
        public async Task<ActionResult<Transactions>> CreateTransaction(Transactions transactions)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<Account>> ViewAccountInformation()
        {
            return null;
        }
    }
}