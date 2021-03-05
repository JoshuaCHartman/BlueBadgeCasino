using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
namespace Casino.WebApi.Controllers
{
    public class GameController : ApiController
    {
        //Post
        /// <summary>
        /// Create a new game - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public IHttpActionResult Post(GameCreate game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateGameService();
            if (!service.CreateGame(game))
                return InternalServerError();
            return Ok();
        }
        //Get
        /// <summary>
        /// Get a list of all games available to play
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            GameService gameService = CreateGameService();
            var games = gameService.GetGames();
            return Ok(games);
        }

        //Player Get
        /// <summary>
        /// Get all games played by a Player, using PlayerID/GUID
        /// </summary>
        /// <returns></returns>
        [Route("api/PlayerGames")]
        [HttpGet]
        public IHttpActionResult PlayerGet()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            GameService gameService = CreateGameService();
            var games = gameService.GetGamesPlayer(userId);
            return Ok(games);
        }


        //Player Bet Limits
        /// <summary>
        /// Enter a bet amount, and discover what games area available to play
        /// </summary>
        /// <returns></returns>
        [Route("api/Show_if_bet_within_limits")]
        [HttpGet]
        public IHttpActionResult BetLimits(double playerBet)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            GameService gameService = CreateGameService();
            var games = gameService.GetGamesPlayer(userId);

            var min = Convert.ToDouble(games.ToArray()[3]);
            var max = Convert.ToDouble(games.ToArray()[4]);

            var bet = playerBet;

            if (bet >= min && bet <= max)
            {
                return Ok();
            }
            else { return BadRequest("Bet must be within game limits."); }
        }

        //Get by ID
        /// <summary>
        /// Get details of games by GameID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            GameService gameService = CreateGameService();
            var game = gameService.GetGameById(id);
            return Ok(game);
        }
        //Put
        /// <summary>
        /// Edit an existing Game - restricted to SuperAdmin, Admin
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public IHttpActionResult Put(GameUpdate game)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateGameService();
            if (!service.UpdateGame(game))
                return InternalServerError();
            return Ok();
        }


        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }

    }
}
