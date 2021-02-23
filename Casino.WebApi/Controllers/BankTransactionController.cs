using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Casino.WebApi.Controllers
{
    [Authorize]
    public class BankTransactionController : ApiController
    {
        private BankTransactionService _bankService = new BankTransactionService();
        private BankTransactionService CreateBankTransactionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bankTransactionService = new BankTransactionService(userId);
            return bankTransactionService;
        }
        //Get
        //Get all by logged in Player
        [Authorize(Roles = "user")]
        public IHttpActionResult Get()
        {
            BankTransactionService bankTransactionService = CreateBankTransactionService();
            var bankTransactions = bankTransactionService.PlayerGetBankTransactions();
            return Ok(bankTransactions);
        }
        //Get by id for logged in player
        [Authorize(Roles = "user")]
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
        [Authorize(Roles = "user")]
        public IHttpActionResult Post(BankTransactionCreate bankTransaction)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBankTransactionService();
            if (!service.CreateBankTransaction(bankTransaction))
                return InternalServerError();
            return Ok();
        }
        //Delete
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Bank/admin/{id}/{amount}")]
        public IHttpActionResult Delete([FromUri]int id, [FromUri]double amount)
        {
            var service = CreateBankTransactionService();
            if (!service.DeleteBankTransaction(id, amount))
                return InternalServerError();
            return Ok();
        }
    }
}
