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

        /// <summary>
        /// Withdrawal transaction creation and action upon an account
        /// </summary>
        /// <param name="accountId">The unique account ID number</param>
        /// <param name="transaction">The withdrawal transaction taking place on the account</param>
        /// <returns>The full account object</returns>
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

        /// <summary>
        /// Deposit transaction creation and action upon an account
        /// </summary>
        /// <param name="accountId">The unique account ID number</param>
        /// <param name="transaction">The deposit transaction taking place on the account</param>
        /// <returns>The full account object</returns>
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

        /// <summary>
        /// Views the properties of an account
        /// </summary>
        /// <param name="id">The unique account ID number</param>
        /// <returns>Account information</returns>
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

        /// <summary>
        /// Views the transaction history for an account
        /// </summary>
        /// <param name="accountId">The unique account ID number</param>
        /// <returns>Transaction list for an account</returns>
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