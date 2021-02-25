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
    public class GameController : ApiController
    {
        //Post
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

        //GamePlay
        public double playGame(int id, double betAmt, Guid playerId)
        {
            double amount = 0;
            var game = new GameService(playerId);

            string gameName = game.GetGameById(id).GameName;
            double payout = 0;

            switch (gameName.ToLower())
            {
                case "baccarat":
                    game.Baccarat();
                    break;
                case "blackjack":
                    game.Blackjack();
                    break;
                case "craps":
                    //bool Pass or Don't Pass bet type
                    //Not quite there yet
                    bool pass = true;
                    game.Craps(pass);
                    break;
                case "roulette":
                    game.baseGame();
                    break;
                case "keno":
                    game.baseGame();
                    break;
                default:
                    break;
            }

            if(payout > 0) { amount = payout * betAmt + betAmt; }
            else { amount = -1 * betAmt; }

            return amount;            
        }
    }
}
