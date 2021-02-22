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
        //Get All by Player
        public IHttpActionResult Get() //Get([FromBody] int playerId)
        {
            //var bets = _service.PlayerGetBets();
            BetService betService = CreateBetService();
            var bets = betService.PlayerGetBets();
            return Ok(bets);
        }
        //Get All by Admin
        [HttpPost]
        [Route("api/Bet/admin")]
        public IHttpActionResult PostbyAdminSearch([FromBody] GetBetByParameters model)
        {
            var bets = _service.AdminGetBets(model);
            return Ok(bets);

        }
        //Get By Id
        public IHttpActionResult GetById(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.GetBetById(id);

            return Ok(bet);
        }
        //Get By Id by Admin
        [HttpGet]
        [Route("api/Bet/admin/guid")]
        public IHttpActionResult GetbyAdminByGuid([FromUri] string guidAsString)
        {
            Guid guid = Guid.Parse(guidAsString);
            var bets = _service.AdminGetBets(guid);
            return Ok(bets);

        }

        //Get By Id by Admin
        [HttpGet]
        [Route("api/Bet/admin/gameId")]
        public IHttpActionResult GetbyAdminByGame([FromUri] int gameId)
        {
            var bets = _service.AdminGetBets(gameId);
            return Ok(bets);

        }

        //Post
        public IHttpActionResult Post(BetCreate bet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBetService();
            if (!service.CreateBet(bet))
                return InternalServerError();
            return Ok();
        }
        //Delete
        public IHttpActionResult Delete([FromUri] int id, [FromUri] double amount)
        {
            var service = CreateBetService();
            if (!service.DeleteBet(id, amount))
                return InternalServerError();
            return Ok();
        }
    }


}
