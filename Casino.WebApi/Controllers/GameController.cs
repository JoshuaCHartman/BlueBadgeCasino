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
        public double PlayGame(int id, double betAmt, Guid playerId)
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
                    //bet types
                    //Straight 1 number
                    //Split 2 numbers adjoining
                    //Street 1 row
                    //Corner 4 numbers adjoining
                    //Double Street 2 rows
                    //Trio 0,1,2 or 00,2,3 (00 = 37 for sanity)
                    //Basket 0,00,1,2,3 (00 = 37 for sanity)
                    //Low < 19
                    //High > 18
                    //Red
                    //Black
                    //Even 
                    //Odd
                    //Dozen 1-12; 13-24; 25-36
                    //Column mod 3 = 1, 2, or 3
                    //Snake 1, 5, 9, 12, 14, 16, 19, 23, 27, 30, 32, 34
                    game.baseGame();
                    break;
                case "keno":
                    //List<int> from "player selection" range = 1-80; up to 20 #'s selected - let's use 10 #'s
                    game.baseGame();
                    break;
                case "russian roulette":
                    game.RussianRoulette();
                    break;
                default:
                    break;
            }
            if (payout > 0) { amount = payout * betAmt + betAmt; }
            else if (payout < 0) { }//accountDelete
            else { amount = -1 * betAmt; }
            return amount;
        }
    }
}
//Model for roll and hit, etc
