using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace Casino.WebApi.Controllers
{
    // can add [RoutePrefix("api/bet")] below first (topmost) [Authorize] and then each endpoint route can be shortened to for example
    // [Route("player")] for Get ALL by Player

    [Authorize]
    public class BetController : ApiController
    {
        private BetService _service = new BetService();

        private BetService CreateBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var betService = new BetService(userId);
            return betService;
        }
        //Get All by Player
        /// <summary>
        /// Get a complete Bet History - restricted to Player
        /// </summary>
        
        /// <returns></returns>
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
        /// <summary>
        /// Get all Bets - restricted to SuperAdmin, Admin
        /// </summary>
        
        /// <returns></returns>
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        [Route("api/bet/admin/all")]
        public IHttpActionResult GetAllByAdmin()
        {
            var bets = _service.AdminGetBets();
            return Ok(bets);
        }

        //Get by model by admin
        /// <summary>
        /// Get Bets from parameters (Player win/lose, Amount, Timespan) - restricted to SuperAdmin, Admin
        /// </summary>
        
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/model")]
        public IHttpActionResult PostbyAdminSearch([FromBody] GetBetByParameters model)
        {
            var bets = _service.AdminGetBets(model);
            return Ok(bets);

        }
        //Player Get By BetId
        /// <summary>
        /// Get Bet by Bet ID - restricted to Player
        /// </summary>
        
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [Route("api/bet/player/{id}")]
        public IHttpActionResult GetById(int id)
        {
            BetService betService = CreateBetService();
            var bet = betService.GetBetById(id);

            return Ok(bet);
        }
        //Get By Guid by Admin
        /// <summary>
        /// Get Bets by PlayerID/GUID - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get Bets by GameID - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/game/{gameId}")]
        public IHttpActionResult GetbyAdminByGame([FromUri] int gameId)
        {
            var bets = _service.AdminGetBets(gameId);
            return Ok(bets);

        }
        //Get By BetId by Admin
        /// <summary>
        /// Get Bets by BetID - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/bet/admin/betId/{betId}")]
        public IHttpActionResult GetByAdminByBetId([FromUri] int betId)
        {
            var bets = _service.AdminGetBetsByBetId(betId);
            return Ok(bets);

        }

        //Post
        /// <summary>
        /// Enter a new bet - restricted to Players
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [Route("api/bet/player/")]
        public IHttpActionResult Post(BetCreate bet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateBetService();
            if (!_service.CheckIfGameIdExists(bet.GameId))
                return BadRequest($"The game with gameId {bet.GameId} does not exist. Please choose another.  For a list of games try the following endpoint: " +
                    "/api/PlayerGames");
            if (!service.CheckPlayerBalance(bet.BetAmount))//confirm player has enough funds
                return BadRequest("Bet Amount Exceeds Player Balance");
            var betResult = service.CreateBet(bet);
            if (betResult is null)
                return BadRequest("Sorry, your bet did not post.  You either tried to play a game outside of your access level, or outside the range of the MinMax bet");
            return Ok(betResult);
        }
        //Delete
        /// <summary>
        /// Delete Bet by BetID - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
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