using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace Casino.WebApi.Controllers
{
    [Authorize]
    public class BankTransactionController : ApiController
    {
        private void SendEmail(BankTransactionCreate bankTransaction)
        {
            // switched to mailtrap.io for testing - use mailtrap dashboard for verification of receipt of emails (works)
            //var smtpClient = new SmtpClient("smtp.emailserver.com")
            var smtpClient = new SmtpClient("smtp.mailtrap.io")
            {
                //Port = xxx expected port of receiving smtp,
                Port = 2525,
                // Credentials = new NetworkCredential("account@account.com", "password"),
                Credentials = new NetworkCredential("40389242ceb32f", "ed8d9f5b311ca1"),
                EnableSsl = true,
            };
            var amount = Convert.ToString(bankTransaction.BankTransactionAmount);
            var userId = Guid.Parse(User.Identity.GetUserId());
            var customer = Convert.ToString(userId);

            smtpClient.Send("automatedbluebadgecasino@bluebadgecasino.com", "accountingbluebadgecasino@bluebadgecasino.com", $"Player withdrawal : ${amount}", $"PlayerId: {customer} wishes to withdraw $ {amount}. Please initiate a bank transfer.");

        }

        private BankTransactionService _bankService = new BankTransactionService();
        private BankTransactionService CreateBankTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bankTransactionService = new BankTransactionService(userId);
            return bankTransactionService;
        }
        //Get
        //Get all by logged in Player
        /// <summary>
        /// Return all Bank Transactions by the currently logged in Player
        /// </summary>

        /// <returns></returns>
        [Authorize(Roles = "User")]
        [Route("api/bank/player")]
        public IHttpActionResult Get()
        {
            BankTransactionService bankTransactionService = CreateBankTransactionService();
            var bankTransactions = bankTransactionService.PlayerGetBankTransactions();
            return Ok(bankTransactions);
        }
        //Get by id for logged in player
        /// <summary>
        /// Return a detailed Bank Transaction by its ID number
        /// </summary>

        /// <returns></returns>

        [Authorize(Roles = "User")]
        [Route("api/bank/player/{id}")]
        public IHttpActionResult GetById(int id)
        {
            BankTransactionService bankTransactionService = CreateBankTransactionService();
            var bankTransaction = bankTransactionService.GetBankTransactionById(id);

            return Ok(bankTransaction);
        }
        //Get all by Admin for Specific player
        /// <summary>
        /// Get all Bank Transaction by PlayerID - restricted to SuperAdmin, Admin
        /// </summary>

        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin/{guidAsString}")]
        public IHttpActionResult Get(string guidAsString)
        {
            Guid guid = Guid.Parse(guidAsString);

            var bankTransactions = _bankService.AdminGetBankTransactions(guid);
            return Ok(bankTransactions);
        }
        //Get all by Admin
        /// <summary>
        /// Return all Bank Transactions - restricted to Admin
        /// </summary>

        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin")]
        public IHttpActionResult GetAll()
        {

            var bankTransactions = _bankService.AdminGetBankTransactions();
            return Ok(bankTransactions);
        }
        //Post
        /// <summary>
        /// Post a deposit Bank Transactions - restricted to Player (left for testing purposes, use Charge method)
        /// </summary>

        /// <returns></returns>
        [Authorize(Roles = "User")]
        [Route("api/bank/player")]
        public IHttpActionResult Post(BankTransactionCreate bankTransaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBankTransactionService();
            if (!service.CreateBankTransaction(bankTransaction))
                return InternalServerError();
            return Ok();
        }
        //Post
        /// <summary>
        /// Make a withdrawal Bank Transactions - restricted to Player
        /// </summary>

        /// <returns></returns>
        [Authorize(Roles = "User")]
        [Route("api/bank/playerWithdraw")]
        public IHttpActionResult Withdraw(BankTransactionCreate bankTransaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBankTransactionService();

            SendEmail(bankTransaction);

            if (!service.CreateBankTransaction(bankTransaction))
                return InternalServerError();

            return Ok($"Your account withdrawal is being initiated in the amount of : $ {bankTransaction.BankTransactionAmount}.");
        }


        //Delete
        /// <summary>
        /// Delete a Bank Transactions - restricted to SuperAdmin, Admin
        /// </summary>

        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin/{id}/{amount}")]
        public IHttpActionResult Delete([FromUri] int id, [FromUri] double amount)
        {
            var service = CreateBankTransactionService();
            if (!service.DeleteBankTransaction(id, amount))
                return InternalServerError();
            return Ok();
        }


    }
}
