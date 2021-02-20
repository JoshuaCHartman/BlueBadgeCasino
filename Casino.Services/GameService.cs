using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Services
{
    //random comment
    public class GameService
    {
        private readonly Guid _userId;

        public GameService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateGame(GameCreate model)
        {
            var entity =
                new Game()
                {
                    GameName = model.GameName,
                    TypeOfGame = model.TypeOfGame,
                    MinBet = model.MinBet,
                    MaxBet = model.MaxBet
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<GameListItem> GetGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Games
                        .Where(e => e.GameId > -1)
                        .Select(
                            e =>
                                new GameListItem
                                {
                                    GameId = e.GameId,
                                    GameName = e.GameName,
                                    TypeOfGame = e.TypeOfGame
                                    
                                }
                        );

                return query.ToArray();
            }
        }
    }


    //    public bool PlayGame(GameInstance model)
    //    {
    //        // Game Logic
    //        bool winner;
    //        Random r = new Random();
    //        // Odds Range 0 - 99
    //        // House Odds 0 - 49
    //        // Player Odds 50 - 99
    //        int i = r.Next(0, 99);
    //        if (i < 50)
    //        {
    //            winner = true;
    //        }
    //        else
    //        {
    //            winner = false;
    //        }

    //        var entity =
    //            new Game()
    //            {
    //                GameId = model.GameID,
    //                TypeOfGame = model.TypeOfGame,
    //                HouseWins = winner,
    //                PayoutMultiplier = 1
    //    };

    //        using (var ctx = new ApplicationDbContext())
    //        {
    //            ctx.Games.Add(entity);
    //            return ctx.SaveChanges() == 1;
    //        }
    //    }
    //}
}
