using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Casino.Services
{
    public class BetService
    {
        private GameSimulation _gameSim = new GameSimulation();
        private readonly Guid _playerGuid;  //Parse from currently logged in player
        private Guid _houseGuid = GetHouseAccountGuid();
        private GameService _gameService = new GameService();

        public BetService() { }
        public BetService(Player player)
        {
        }
        public BetService(Guid userGuid)
        {
            _playerGuid = userGuid;
        }
        public BetResult CreateBet(BetCreate model)
        {

            double payout;
            var hasAccess = CheckPlayerAccess();
            // Brought _gameSim play game mechanics outside, and captured result as variable.
            //      That result will be fed into added helper method (in gamesimulation.cs) to derive win/loss bool.
            //      Now both PayoutAmount and PlayerWonGame derived
            //      from _gameSim.
            //double payout = _gameSim.PlayGame(model.BetAmount, model.GameId);
            if (!model.TypeOfBet.HasValue)
                payout = _gameService.PlayGame(model.GameId, model.BetAmount, hasAccess);
            else
                payout = _gameService.PlayGame(model.GameId, model.BetAmount, hasAccess, (GameService.BetType)model.TypeOfBet);
            if (payout == 0)
                return null;
            var entity = new Bet()
            {

                PlayerId = _playerGuid,
                GameId = model.GameId,
                BetAmount = model.BetAmount,
                PayoutAmount = payout,
                PlayerWonGame = _gameSim.GameWinOutcome(payout),
                DateTimeOfBet = DateTimeOffset.Now,
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bets.Add(entity);
                if (ctx.SaveChanges() != 0 && UpdatePlayerBankBalance(_playerGuid, entity.PayoutAmount) && UpdateHouseBankBalance(entity.PayoutAmount * (-1)))
                {
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
                        .Where(e => e.BetId > -1)
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
                //F F F 
                if (model is null) //this will return ALL bets EVER
                {

                    var query =
                        ctx
                            .Bets
                            .Where(e => e.GameId > 0)
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
                //All 3 parameters
                //T T T 
                if (model.PlayerWonGame.HasValue && model.Time.HasValue && model.BetAmount.HasValue)
                {
                    int noOfDays = (int)model.Time;
                    DateTimeOffset date = DateTimeOffset.Now.Subtract(new TimeSpan(noOfDays, 0, 0, 0));
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.PlayerWonGame == model.PlayerWonGame && e.DateTimeOfBet > date && e.BetAmount >= model.BetAmount)
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
                //T T F

                if (model.PlayerWonGame.HasValue && model.Time.HasValue && !model.BetAmount.HasValue)
                {
                    int noOfDays = (int)model.Time;
                    DateTimeOffset date = DateTimeOffset.Now.Subtract(new TimeSpan(noOfDays, 0, 0, 0));
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.PlayerWonGame == model.PlayerWonGame && e.DateTimeOfBet > date)
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
                //T F T
                if (model.PlayerWonGame.HasValue && !model.Time.HasValue && model.BetAmount.HasValue)
                {
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.PlayerWonGame == model.PlayerWonGame && e.BetAmount >= model.BetAmount)
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
                //T F F 
                if (model.PlayerWonGame.HasValue && !model.Time.HasValue && !model.BetAmount.HasValue)
                {
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.PlayerWonGame == model.PlayerWonGame)
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
                //F T T
                if (!model.PlayerWonGame.HasValue && model.Time.HasValue && model.BetAmount.HasValue)
                {
                    int noOfDays = (int)model.Time;
                    DateTimeOffset date = DateTimeOffset.Now.Subtract(new TimeSpan(noOfDays, 0, 0, 0));
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.DateTimeOfBet > date && e.BetAmount >= model.BetAmount)
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
                //F F T
                if (!model.PlayerWonGame.HasValue && !model.Time.HasValue && model.BetAmount.HasValue)
                {
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.BetAmount >= model.BetAmount)
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
                // F T F
                if (!model.PlayerWonGame.HasValue && model.Time.HasValue && !model.BetAmount.HasValue)
                {
                    int noOfDays = (int)model.Time;
                    DateTimeOffset date = DateTimeOffset.Now.Subtract(new TimeSpan(noOfDays, 0, 0, 0));
                    var query =
                        ctx
                            .Bets
                            .Where(e => e.DateTimeOfBet > date)
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

                return null;
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

        public BetDetail GetBetById(int id)
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
                if (UpdatePlayerBankBalance(entity.PlayerId, entity.PayoutAmount * (-1)) && ctx.SaveChanges() == 1)
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
                entity.CurrentBankBalance = entity.CurrentBankBalance + amount;
                return ctx.SaveChanges() == 1;
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
                entity.CurrentBankBalance = entity.CurrentBankBalance + amount;
                return ctx.SaveChanges() == 1;
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
        public bool CheckPlayerAccess()
        {
            var ctx = new ApplicationDbContext();
            var entity =
            ctx.Players


                .Single(e => e.PlayerId == _playerGuid);
            return entity.HasAccessToHighLevelGame;
        }

        public bool CheckIfGameIdExists(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Games.Find(id);
                if (query is null)
                    return false;
            }
            return true;
        }

        // returns BetResult Model afet BetCreate
        public BetResult GetBetResult(int id)
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

