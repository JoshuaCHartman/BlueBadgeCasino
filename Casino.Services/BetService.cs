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
        private readonly Guid _playerGuid;  //Parse from currently logged in player
        private readonly Player _currentPlayer;
        private readonly int _playerId;

        public BetService() { }
        public BetService(Player player)//try this way or see other version just below
        {
            //_playerId = player.PlayerId;
        }
        public BetService(Guid userGuid)
        {

            _playerGuid = userGuid;
        }
        public bool CreateBet(BetCreate model)
        {

            var entity = new Bet()
            {
                // PlayerId = _playerId; //if we go this route need to add Guid to Bet class
                //PlayerId = Player.PlayerId;
                PlayerId = model.PlayerId,
                GameId = model.GameId,
                BetAmount = model.BetAmount,
                PlayerWonGame = _gameSim.PlayerWonGame(model.GameId),
                DateTimeOfBet = DateTimeOffset.Now

            };
            using (var ctx = new ApplicationDbContext())
            {

                ctx.Bets.Add(entity);
                return ctx.SaveChanges() != 0;
            }

        }

        public IEnumerable<BetListItem> PlayerGetBets()//PlayerGetBets(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.PlayerId == _playerId)
                        //.Where(e => e.PlayerId == playerId)//from argument in method
                        .Select(
                            e =>
                                new BetListItem
                                {
                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    DateTimeOfBet = e.DateTimeOfBet,
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }
        public BetDetail GetBetById(int id) //if this looks identical to BetListItem we can call that model instead of having 2
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bets
                        .Single(e => e.PlayerId == _playerId && e.BetId == id);
                return
                    new BetDetail

                    {
                        BetId = entity.BetId,
                        PlayerId = entity.PlayerId,
                        DateTimeOfBet = entity.DateTimeOfBet,
                        GameId = entity.GameId,
                        BetAmount = entity.BetAmount,
                        PlayerWonGame = entity.PlayerWonGame,
                        PayoutAmount = entity.PayoutAmount
                    };

            }
        }
    }

}