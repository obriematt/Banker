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
        private readonly IBankService _bankService;

        public AccountController(BankLedgerContext context, IBankService bankService)
        {
            _context = context;
            _bankService = bankService;
        }

        [HttpPatch("withdraw/{id}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Account> WithdrawFromAccount(int accountId, [FromBody] Transactions transaction)
        {
            var withdrawCreated = _bankService.WithdrawlFromAccount(accountId, transaction);
            if(withdrawCreated == null)
            {
                return BadRequest();
            }
            return withdrawCreated;
        }

        [HttpPatch("deposit/{id}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Account> DepositIntoAccount(int accountId, [FromBody] Transactions transaction)
        {
            var depositCreated = _bankService.DepositIntoAccount(accountId, transaction);
            if(depositCreated == null)
            {
                return BadRequest();
            }
            return depositCreated;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Account> ViewAccountInformation(int id)
        {
            var accountFound = _bankService.GetAccount(id);
            if(accountFound == null)
            {
                return NotFound();
            }

            return accountFound;
        }

        [HttpGet("transactions/{id}")]
        [ProducesResponseType(typeof(IEnumerable<Transactions>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Transactions>> ViewTransactionHistoryForAccount(int accountId)
        {
            var transactions = _bankService.GetAllTransactionsForAccount(accountId);
            if(transactions == null)
            {
                return NotFound();
            }
            return transactions.ToList();
        }
    }
}