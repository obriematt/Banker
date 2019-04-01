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
        private readonly IBankService _bankService;

        public ManagerAccountController(BankLedgerContext context, IBankService bankService)
        {
            _context = context;
            _bankService = bankService;
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount([FromBody] Account account)
        {
            var accountCreated = _bankService.CreateAccount(account);
            if (accountCreated == null)
            {
                return BadRequest();
            }
            return accountCreated;
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteAccount(int id)
        {
            var accountDeleted = _bankService.DeleteAccount(id);
            if(!accountDeleted)
            {
                return BadRequest();
            }
            return accountDeleted;
        }

        [HttpGet("transactions")]
        public ActionResult<IEnumerable<Transactions>> ViewBankTransactionHistory()
        {
            var fullTransactionHistory = _bankService.GetAllTransactions();
            if(fullTransactionHistory == null)
            {
                return BadRequest();
            }
            return fullTransactionHistory.ToList();
        }

        [HttpGet("accounts")]
        public ActionResult<IEnumerable<Account>> ViewAllAccounts()
        {
            var allAccounts = _bankService.GetAccounts();
            if(allAccounts == null)
            {
                return BadRequest();
            }
            return allAccounts.ToList();
        }
    }
}
