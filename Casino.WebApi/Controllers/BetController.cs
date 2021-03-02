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
    // can add [RoutePrefix("api/bet")] below first (topmost) [Authorize] and then each endpoint route can be shortened to for example
    // [Route("player")] for Get ALL by Player

    [Authorize] //do we need this if we don't use Guid.  Do we need this for PlayerController? // Yup -JCH
    public class BetController : ApiController
    {
        private BetService _service = new BetService(); // Can we do this instead of the private method way below?

        private BetService CreateBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var betService = new BetService(userId);
            return betService;
        }
        //Get All by Player
        [Authorize(Roles = "User")]
        [Route("api/bet/player")]
        public IHttpActionResult Get() //Get([FromBody] int playerId)
        {
            //var bets = _service.PlayerGetBets();
            BetService betService = CreateBetService();
            var bets = betService.PlayerGetBets();
            return Ok(bets);
        }
        //Get All by Admin
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        [Route("api/bet/admin/all")]
        public IHttpActionResult GetAllByAdmin()
        {
            var bets = _service.AdminGetBets();
            return Ok(bets);
        }

        //Get by model by admin
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/model")]
        public IHttpActionResult PostbyAdminSearch([FromBody] GetBetByParameters model)
        {
            var bets = _service.AdminGetBets(model);
            return Ok(bets);

        }
        //Player Get By BetId
        [Authorize(Roles = "User")]
        [Route("api/bet/player/{id}")]
        public IHttpActionResult GetById(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.GetBetById(id);

            return Ok(bet);
        }
        //Get By Guid by Admin
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/guid/{guidAsString}")]
        public IHttpActionResult GetbyAdminByGuid([FromUri] string guidAsString)
        {
            Guid guid = Guid.Parse(guidAsString);
            var bets = _service.AdminGetBets(guid);
            return Ok(bets);

        }

        //Get By GameId by Admin
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/game/{gameId}")]
        public IHttpActionResult GetbyAdminByGame([FromUri] int gameId)
        {
            var bets = _service.AdminGetBets(gameId);
            return Ok(bets);

        }
        //Get By BetId by Admin
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/betId/{betId}")]
        public IHttpActionResult GetByAdminByBetId([FromUri] int betId)
        {
            var bets = _service.AdminGetBetsByBetId(betId);
            return Ok(bets);

        }

        //Post
        [Authorize(Roles = "User")]
        [Route("api/bet/player/")]
        public IHttpActionResult Post(BetCreate bet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBetService();
            if (!service.CheckPlayerBalance(bet.BetAmount))//confirm player has enough funds
                return BadRequest("Bet Amount Exceeds Player Balance");
            var betResult = service.CreateBet(bet);
            if (betResult is null)
                return BadRequest("Sorry, we are not sure what went wrong");
            return Ok(betResult);
        }
        //Delete
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/{id}/{amount}")]
        public IHttpActionResult Delete([FromUri] int id, [FromUri] double amount)
        {
            var service = CreateBetService();
            if (!service.DeleteBet(id, amount))
                return InternalServerError();
            return Ok();
        }
    }


}
