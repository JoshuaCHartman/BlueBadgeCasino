using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Services
{
    public class BetService
    {
        private GameSimulation _gameSim = new GameSimulation();
        private readonly Guid _playerId;  //Parse from currently logged in player
        private readonly Player _currentPlayerId;
        public BetService(Player player)//try this way or see other version just below
        {
           // _currentPlayerId = player.PlayerId;
        }
        public BetService(Guid userId)
        {

        }
    public bool CreateBet(BetCreate model)
    {

        var entity = new Bet()
        {
            // PlayerId = _playerId; //if we go this route need to add Guid to Bet class
            //PlayerId = _currentPlayerId;
            //PlayerId = Player.PlayerId;
            PlayerId = model.PlayerId,
            GameId = model.GameId,
            BetAmount = model.BetAmount,
            PlayerWonGame = _gameSim.PlayerWonGame(model.GameId),

        };
        using (var ctx = new ApplicationDbContext())
        {

            ctx.Bets.Add(entity);
            return ctx.SaveChanges() != 0;
        }

    }
}

    }