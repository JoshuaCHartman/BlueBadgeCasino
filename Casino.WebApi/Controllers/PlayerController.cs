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


        public IHttpActionResult GetAllPlayers()
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayers();
            return Ok(player);
        }

        public IHttpActionResult GetById(Guid id)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerById(id);
            return Ok(player);
        }

        public IHttpActionResult GetSelf()
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetSelf();
            return Ok(player);
        }


        public IHttpActionResult GetByTierStatus(TierStatus TierStatus)
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerByTierStatus(TierStatus);
            return Ok(player);
        }

        public IHttpActionResult GetPlayerByCurrentBalance()
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetPlayerByCurrentBalance();
            return Ok(player);
        }

        public IHttpActionResult GetActivePlayers(bool IsActive)
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

        public IHttpActionResult Put(PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.UpdatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        // UpdatePlayerByAdmin
        public IHttpActionResult Put(PlayerEdit model, Guid playerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.UpdatePlayerByAdmin(model, playerId))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete() 
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer())
                return InternalServerError();

            return Ok();
        }
    }
}
