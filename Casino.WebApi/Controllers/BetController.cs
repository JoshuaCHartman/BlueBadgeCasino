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
        [Authorize] //do we need this if we don't use Guid.  Do we need this for PlayerController?
    public class BetController : ApiController
    {
        private BetService _service = new BetService(); // Can we do this instead of the private method way below?

        private BetService CreateBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var betService = new BetService(userId);
            return betService;
        }
        //Get All
        public IHttpActionResult Get() //Get([FromBody] int playerId)
        {
            //var bets = _service.PlayerGetBets();
            BetService betService = CreateBetService();
            var bets = betService.PlayerGetBets();
            return Ok(bets);
        }
        //Get By Id
        public IHttpActionResult GetById(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.GetBetById(id);

            return Ok(bet);

        }
        //Post
        public IHttpActionResult Post(BetCreate bet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBetService();
            if (!service.CreateBet(bet))
                return InternalServerError();
            return Ok("Bet complete");

            //NOT WORKING : (returns all You LOST 0) 
            // 
            //if (bet.PlayerWonGame == true)
            //    return Ok($"You WON { bet.PayoutAmount}");
            //else
            //    return Ok($"You LOST { bet.BetAmount}");

        }
        //Delete
        public IHttpActionResult Delete([FromUri]int id, [FromBody] double amount)
        {
            var service = CreateBetService();
            if (!service.DeleteBet(id, amount))
                return InternalServerError();
            return Ok();
        }
    }


}
