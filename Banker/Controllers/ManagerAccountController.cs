using Banker.Contexts;
using Banker.Models;
using Banker.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerAccountController : ControllerBase
    {
        private readonly BankLedgerContext _context;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public ManagerAccountController(BankLedgerContext context, ITransactionService transactionService, IAccountService accountService)
        {
            _context = context;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            return null;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> ViewBankTransactionHistory()
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> ViewAllAccounts()
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> ViewAllTransactionsForAccount(Account account)
        {
            return null;
        }
    }
}
