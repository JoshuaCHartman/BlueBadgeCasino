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
        
        [HttpGet]
        public IHttpActionResult Get()
        {
            GameService gameService = CreateGameService();
            var games = gameService.GetGames();
            return Ok(games);
        }

        //Player Get
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
        [Route("api/Play")]
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
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            GameService gameService = CreateGameService();
            var game = gameService.GetGameById(id);
            return Ok(game);
        }
        //Put
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
