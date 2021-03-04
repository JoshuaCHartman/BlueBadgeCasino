using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
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
        private GameService CreateGameService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var gameService = new GameService(userId);
            return gameService;
        }

    }
}
