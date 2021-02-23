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
        private PlayerService _service = new PlayerService();

        //Player gets own player info
        [Authorize(Roles = "User")]
        [Route("api/Player/")]
        public IHttpActionResult GetSelf()
        {
            PlayerService playerService = CreatePlayerService();
            var player = playerService.GetSelf();
            return Ok(player);
        }
        //Admin get all players
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin")]
        public IHttpActionResult GetAll()
        {
            var players = _service.GetPlayers();
            return Ok(players);
        }

        //Admin gets player by Guid
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/guid/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var player = _service.GetPlayerById(id);
            return Ok(player);
        }
        //Admin gets players by Tier
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/tier/{tierStatus}")]
        public IHttpActionResult Get(TierStatus tierStatus)
        {
            var players = _service.GetPlayerByTierStatus(tierStatus);
            return Ok(players);
        }
        //Admin get players with balance
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/balance")]
        public IHttpActionResult GetPlayerHasBalance()
        {
            var player = _service.GetPlayerByHasBalance();
            return Ok(player);
        }
        //Admin Get active players
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/active")]
        public IHttpActionResult GetActivePlayers()
        {
            var player = _service.GetActivePlayers();
            return Ok(player);
        }

        //User creates player account
        [Authorize(Roles = "User")]
        public IHttpActionResult Post(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }
        //Player Deletes account(just makes it inactive)
        [Authorize(Roles = "User")]
        public IHttpActionResult Delete()
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer())
                return InternalServerError();

            return Ok();
        }
    }
}
