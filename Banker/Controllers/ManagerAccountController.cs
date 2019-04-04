using Banker.Contexts;
using Banker.Models;
using Banker.Services;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="account">The account object to add to the bank</param>
        /// <returns>Boolean value for success or failure</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Account> CreateAccount([FromBody] Account account)
        {
            var accountCreated = _bankService.CreateAccount(account);
            if (accountCreated == null)
            {
                return BadRequest();
            }
            return Ok(accountCreated);
        }

        /// <summary>
        /// Deletes an account from the bank
        /// </summary>
        /// <param name="id">The unique account ID number</param>
        /// <returns>Boolean value for successful or failure</returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> DeleteAccount(int id)
        {
            var accountDeleted = _bankService.DeleteAccount(id);
            if(!accountDeleted)
            {
                return BadRequest();
            }
            return accountDeleted;
        }

        /// <summary>
        /// View all transaction history for all accounts
        /// </summary>
        /// <returns>List of Transactions</returns>
        [HttpGet("transactions")]
        [ProducesResponseType(typeof(IEnumerable<Transactions>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Transactions>> ViewBankTransactionHistory()
        {
            var fullTransactionHistory = _bankService.GetAllTransactions();
            if(fullTransactionHistory == null)
            {
                return BadRequest();
            }
            return fullTransactionHistory.ToList();
        }

        /// <summary>
        /// View all accounts and information
        /// </summary>
        /// <returns>List of Accounts</returns>
        [HttpGet("accounts")]
        [ProducesResponseType(typeof(IEnumerable<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
