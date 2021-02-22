using Casino.Data;
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
    public class PlayerController : ApiController
    {
        private PlayerService CreatePlayerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var playerService = new PlayerService(userId);
            return playerService;
        }


        public IHttpActionResult Get()
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayers();
            return Ok(player);
        }

        public IHttpActionResult Get(Guid id)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerById(id);
            return Ok(player);
        }

        public IHttpActionResult Get(TierStatus TierStatus)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerByTierStatus(TierStatus);
            return Ok(player);
        }

        public IHttpActionResult Get(double CurrentBankBalance)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerByBalance(CurrentBankBalance);
            return Ok(player);
        }

        public IHttpActionResult Get(bool IsActive)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetActivePlayers(IsActive);
            return Ok(player);
        }


        public IHttpActionResult Post(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(Guid id) 
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer(id))
                return InternalServerError();

            return Ok();
        }
    }
}
