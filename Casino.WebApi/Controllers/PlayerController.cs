using Casino.Data;
using Casino.Models;
using Casino.Services;
using Microsoft.AspNet.Identity;
using System;
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

            //      if (Bets[Bets.Count - 1].Date - DateTime.Now < 6months)
            //return true;

            //                  else return false;


            //Do we want to go with BankTransaction for last date of activity
            //Do we want to return the player information or BadRequest
            //Go off of DateTimeOfBet
            //if (!playerService.CheckActiveStatus(player))
            //{
            //    return BadRequest("Your player status is inactive.");
            //}
            //else
            //{

            return Ok(player);

        }


        //Admin get all players-Note: Might need a loop
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin")]
        public IHttpActionResult GetAllPlayers()
        {
            PlayerService playerService = CreatePlayerService();
            var players = _service.GetPlayers();
            return Ok(players);

            //List of players
            //Model for list of players - PlayerListItem
            //Property for player status
            //Assign a value to that property
            //For each loop on the list
            //CheckActiveStatus Method
            //Status property set
            //Return list of players

            //foreach (PlayerListItem player in players)
            //{
            //    if (!playerService.CheckActiveStatusAdmin(player))
            //    {
            //        return BadRequest("Your player status is inactive.");
            //    }

            //    else
            //    {
            //        return Ok(player);
            //    } 
            //
        }




        //Admin gets player by Guid
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/guid/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            PlayerService playerService = CreatePlayerService();
            var player = _service.GetPlayerById(id);

            //Do we want to go with BankTransaction for last date of activity
            //Do we want to return the player information or BadRequest
            //Go off of DateTimeOfBet
            //if (!playerService.CheckActiveStatus(player))
            //{
            //    return BadRequest("Your player status is inactive.");
            //}
            //else
            //{
            return Ok(player);
            //}
        }

        //Admin gets players by Tier
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/tier/{tierStatus}")]
        public IHttpActionResult GetByTierStatus(TierStatus tierStatus)
        {
            PlayerService playerService = CreatePlayerService();
            var player = _service.GetPlayerByTierStatus(tierStatus);
            return Ok(player);

            //Do we want to go with BankTransaction for last date of activity
            //Do we want to return the player information or BadRequest
            //Go off of DateTimeOfBet
            //if (!playerService.CheckActiveStatus(player))
            //{
            //    return BadRequest("Your player status is inactive.");
            //}
            //else
            //{

            //}
        }

        //Admin get players with balance
        [Authorize(Roles = "Admin, SuperAdmin")]
        [Route("api/Player/admin/balance")]
        public IHttpActionResult GetPlayerHasBalance()
        {
            PlayerService playerService = CreatePlayerService();
            var player = _service.GetPlayerByHasBalance();
            return Ok(player);

            //Do we want to go with BankTransaction for last date of activity
            //Do we want to return the player information or BadRequest
            //Go off of DateTimeOfBet
            //if (!playerService.CheckActiveStatus(player))
            //{
            //    return BadRequest("Your player status is inactive.");
            //}
            //else
            //{
            //    return Ok(player);
            //}
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
        //[Authorize(Roles = "User")]
        [HttpPost]
        [Route("api/makePlayer")]
        public IHttpActionResult Post(PlayerCreate player)  //*BRIAN* looks like it will never get beyond that first bool check with all the "else" returning "ok"
        {

            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.CheckPlayer(player))
                //{
                return BadRequest("Date of birth has been entered in the incorrect format.  Please enter Date of Birth in the format of MM/DD/YYYY.");
       
                    if (!service.CheckDob(player))  //Is this false or does it need to be revised.  If service.checkplayer = false
                    {
                        return BadRequest("You are not 21 and can not create a player.");
                    }
                    
                    if (!service.CreatePlayer(player))
                    {
                        return InternalServerError();
                    }
                   // else
                        return Ok();
                }

        //Player Deletes account(just makes it inactive)
        [Authorize(Roles = "User")]
        [Route("api/Player/delete")]
        public IHttpActionResult Delete()
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer())
                return InternalServerError();

                    return Ok();
                }
            }
        }
