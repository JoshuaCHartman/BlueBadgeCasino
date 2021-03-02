﻿using Casino.Models;
using Casino.Services;
using Casino.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Stripe;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Net.Mail;
using Casino.Data;

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
            
            //var sendGridClient = new SendGridClient("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");
            //var from = new EmailAddress("joshuaCHartman", "Joshua Hartman");
            //var subject = "Withdraw from customer";
            //var to = new EmailAddress("joshuachartman@gmail.com", "Back of House Accounting");
            //var plainContent = "send money to customer";
            //var htmlContent = "<h1>Withdrawal</h1>";
            //var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
            //sendGridClient.SendEmailAsync(mailMessage);

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
        [Authorize(Roles = "User")]
        [Route("api/bank/player")]
        public IHttpActionResult Get()
        {
            BankTransactionService bankTransactionService = CreateBankTransactionService();
            var bankTransactions = bankTransactionService.PlayerGetBankTransactions();
            return Ok(bankTransactions);
        }
        //Get by id for logged in player
        [Authorize(Roles = "User")]
        [Route("api/bank/player/{id}")]

        public IHttpActionResult GetById(int id)
        {
            BankTransactionService bankTransactionService = CreateBankTransactionService();
            var bankTransaction = bankTransactionService.GetBankTransactionById(id);

            return Ok(bankTransaction);
        }
        //Get all by Admin for Specific player
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
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin")]
        public IHttpActionResult GetAll()
        {

            var bankTransactions = _bankService.AdminGetBankTransactions();
            return Ok(bankTransactions);
        }
        //Post
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
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin/{id}/{amount}")]
        public IHttpActionResult Delete([FromUri] int id, [FromUri] double amount)
        {
            var service = CreateBankTransactionService();
            if (!service.DeleteBankTransaction(id, amount))
                return InternalServerError();
            return Ok();
        }

        //    public async Task<dynamic> Pay(RevisedChargeModel charge)
        //    {
        //        return await MakeChargeService.ChargeAsync(charge.CardNumber, charge.Month, charge.Year, charge.Cvc, charge.Zip, charge.Value);
        //    }
        //}


    }
}
