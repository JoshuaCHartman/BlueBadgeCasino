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
        ApplicationDbContext _db = new ApplicationDbContext();
        private GameSimulation _gameSim = new GameSimulation();
        private readonly Guid _playerGuid;  //Parse from currently logged in player
        private Guid _houseGuid = GetHouseAccountGuid();

        public BetService() { }
        public BetService(Player player)//try this way or see other version just below
        {
            //_playerId = player.PlayerId;
        }
        public BetService(Guid userGuid)
        {

            _playerGuid = userGuid;
        }
        public BetResult CreateBet(BetCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Games.Find(model.GameId);
                if (query is null)
                    return null;
            }
            // Brought _gameSim play game mechanics outside, and captured result as variable.
            //      That result will be fed into added helper method (in gamesimulation.cs) to derive win/loss bool.
            //      Now both PayoutAmount and PlayerWonGame derived
            //      from _gameSim.
            double payout = _gameSim.PlayGame(model.BetAmount, model.GameId);

            //consider returning bet results and updated player balance instead of bool
            var entity = new Bet()
            {

                PlayerId = _playerGuid,
                GameId = model.GameId,
                BetAmount = model.BetAmount,
                //PayoutAmount = _gameSim.PlayGame(model.BetAmount, model.GameId),
                PayoutAmount = payout,
                //PlayerWonGame = ( > 0), ///added to Bet class prop logic
                PlayerWonGame = _gameSim.GameWinOutcome(payout),
                DateTimeOfBet = DateTimeOffset.Now,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bets.Add(entity);
                if (ctx.SaveChanges() != 0 && UpdatePlayerBankBalance(_playerGuid, entity.PayoutAmount) && UpdateHouseBankBalance(entity.PayoutAmount * (-1)))
                {
                    // calls down to helper method to update Player balance after bet has processed

                    //return new BetDetail

                    //{
                    //    TimeOfBet = entity.DateTimeOfBet.ToString("M/d/yy/h:m"),
                    //    BetId = entity.BetId,
                    //    GameId = entity.GameId,
                    //    BetAmount = entity.BetAmount,
                    //    PlayerWonGame = entity.PlayerWonGame,
                    //    PayoutAmount = entity.PayoutAmount
                    //};


                    return GetBetResult(entity.BetId);

                }
                return null;
            }
        }
        //player get all bets
        public IEnumerable<BetListItem> PlayerGetBets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.PlayerId == _playerGuid)
                        .Select(
                            e =>
                                new BetListItem
                                {
                                    PlayerId = e.PlayerId,
                                    BetId = e.BetId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToList();
            }
        }
        //admin get ALL bets
        public IEnumerable<BetListItem> AdminGetBets()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.BetId > -1) //&& model.Time < (DateTimeOffset.Now - e.DateTimeOfBet).Days)
                                                  //I want this to check if model contains prop and if not, ignore that paramater**meaning if model was empty then it would return ALL
                        .Select(
                            e =>
                                new BetListItem
                                {

                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }

        //admin get bets by search paramaters model
        public IEnumerable<BetListItem> AdminGetBets(GetBetByParameters model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.PlayerId == model.PlayerId && e.GameId == model.GameId && e.PlayerWonGame == model.PlayerWonGame) //&& model.Time < (DateTimeOffset.Now - e.DateTimeOfBet).Days)
                                                                                                                                        //I want this to check if model contains prop and if not, ignore that paramater**meaning if model was empty then it would return ALL
                        .Select(
                            e =>
                                new BetListItem
                                {
                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }
        //admin get bets by playerid
        public IEnumerable<BetListItem> AdminGetBets(Guid playerId)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.PlayerId == playerId)

                        .Select(
                            e =>
                                new BetListItem
                                {
                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }
        //admin get bets by gameid
        public IEnumerable<BetListItem> AdminGetBets(int gameId)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.GameId == gameId)
                        .Select(

                            e =>
                                new BetListItem
                                {
                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }

        //Admind get by betId
        //admin get bets by gameid
        public IEnumerable<BetListItem> AdminGetBetsByBetId(int betId)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Bets
                        .Where(e => e.BetId == betId)
                        .Select(

                            e =>
                                new BetListItem
                                {
                                    BetId = e.BetId,
                                    PlayerId = e.PlayerId,
                                    TimeOfBet = e.DateTimeOfBet.ToString(),
                                    GameId = e.GameId,
                                    BetAmount = e.BetAmount,
                                    PlayerWonGame = e.PlayerWonGame,
                                    PayoutAmount = e.PayoutAmount
                                }
                        );

                return query.ToArray();
            }
        }
        //Player get bet by id

        public BetDetail GetBetById(int id) //if this looks identical to BetListItem we can call that model instead of having 2
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bets
                        .Single(e => e.PlayerId == _playerGuid && e.BetId == id);
                return
                    new BetDetail

                    {
                        TimeOfBet = entity.DateTimeOfBet.ToString("M/d/yy/h:m"),
                        BetId = entity.BetId,
                        GameId = entity.GameId,
                        BetAmount = entity.BetAmount,
                        PlayerWonGame = entity.PlayerWonGame,
                        PayoutAmount = entity.PayoutAmount,

                    };
            }
        }
        public bool DeleteBet(int id, double amount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                       .Bets
                       .Single(e => e.BetId == id && e.BetAmount == amount);
                ctx.Bets.Remove(entity);
                if (UpdatePlayerBankBalance(entity.PlayerId, entity.PayoutAmount * (-1)) && ctx.SaveChanges() == 1)// && UpdateHouseBankBalance(entity.PayoutAmount))
                    return true;
                return false;
            }
        }

        //Helper Method
        //if this works, copy or ref this for banktransactions also
        private bool UpdatePlayerBankBalance(Guid playerId, double amount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                       .Players
                       .Single(e => e.PlayerId == playerId);
                entity.CurrentBankBalance = entity.CurrentBankBalance + amount; //can we change only this one category
                return ctx.SaveChanges() == 1;                 //should we just call the Put method from here instead?
            }
        }

        private bool UpdateHouseBankBalance(double amount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                       .Players
                       .Single(e => e.PlayerId == _houseGuid);
                entity.CurrentBankBalance = entity.CurrentBankBalance + amount; //can we change only this one category
                return ctx.SaveChanges() == 1;                 //should we just call the Put method from here instead?
            }

        }

        private static Guid GetHouseAccountGuid()
        {
            var ctx = new ApplicationDbContext();
            var entity =
            ctx.Users


                .Single(e => e.Email == "house@casino.com");
            return Guid.Parse(entity.Id);
        }

        public bool CheckPlayerBalance(double bet)
        {
            var ctx = new ApplicationDbContext();
            var entity =
            ctx.Players


                .Single(e => e.PlayerId == _playerGuid);
            if (entity.CurrentBankBalance > bet)
                return true;
            return false;
        }
        // returns BetResult Model afet BetCreate
        public BetResult GetBetResult(int id) //if this looks identical to BetListItem we can call that model instead of having 2
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Bets
                        .Single(e => e.BetId == id);
                return
                    new BetResult

                    {
                        TimeOfBet = entity.DateTimeOfBet.ToString("M/d/yy/h:m"),
                        BetId = entity.BetId,
                        GameId = entity.GameId,
                        BetAmount = entity.BetAmount,
                        PlayerWonGame = entity.PlayerWonGame,
                        PayoutAmount = entity.PayoutAmount,
                        PlayerBankBalance = entity.Player.CurrentBankBalance

                    };
            }
        }
    }
}

