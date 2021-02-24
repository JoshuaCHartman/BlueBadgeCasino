using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Services
{
    public class GameService
    {
        Random r = new Random();
        private int sum = 0;
        public float payout = 0;
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

        public GameListItem GetGameById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Games
                        .Single(e => e.GameId == id);
                return
                    new GameListItem
                    {
                        GameId = entity.GameId,
                        GameName = entity.GameName,
                        MinBet = entity.MinBet,
                        MaxBet = entity.MaxBet
                    };
            }
        }

        public int baseGame()
        {
            int payoutMult = 0;
            // Game Logic
            bool winner;
            Random r = new Random();
            // Odds Range 0 - 99
            // House Odds 0 - 49
            // Player Odds 50 - 99
            int i = r.Next(0, 99);
            if (i < 50)
            {
                winner = true;
            }
            else
            {
                winner = false;
            }

            if (winner)
            {
                payoutMult = 1;
            }
            else { payoutMult = 0; }

            return payoutMult;
        }


        //Game Logic
        //Returns Payout multiplier
        
        private List<int> Deal(int cardsPerHand)
        {
            List<int> deal = new List<int>();
            int i = 0;
            for (i = 1; i < cardsPerHand + 1; i++)
            {
                r = new Random();
                int v = r.Next(1, 13);
                deal.Add(v);
            }

            return deal;
        }

        private List<int> Hit(List<int> hand)
        {
            Random r = new Random();
            int v = r.Next(1, 13);
            hand.Add(v);

            return hand;
        }

        private List<int> Roll()
        {
            List<int> roll = new List<int>();

            return roll;
        }

        private int Spin(int chances)
        {
            int spin = r.Next(1,chances);

            return spin;
        }

        private string Winner(int house, int player)
        {
            if (house < player) { return "Player"; }
            else if (player == house) { return "Tie"; }
            else { return "House"; }
        }

        private int EvaluateBaccarat(List<int> hand)
        {
            sum = 0;
            foreach (int c in hand)
            {
                int card = hand[c];
                hand.Remove(card);
                if (card > 9) { card = 0; }
                hand.Add(card);
            }

            sum = hand.Sum();

            return sum;
        }

        public float Baccarat()
        {
            List<int> houseHand = Deal(2);
            List<int> playerHand = Deal(2);
            string win = "";
            //private method to eval hand(s)

            //Value houseHand
            int houseSum = EvaluateBaccarat(houseHand) % 10;

            //Value playerHand
            int playerSum = EvaluateBaccarat(playerHand) % 10;

            //Game Logic

            //Drawing rule
            if (playerSum >= 8 || houseSum >= 8)
            {
                
            }

            //Player rule
            else if (playerSum <= 5)
            {
                playerHand = Hit(playerHand);
                playerSum = EvaluateBaccarat(playerHand) % 10;
            }

            //Banker rule
            else if (playerHand.Count()==2)
            {
                houseHand = Hit(houseHand);
                houseSum = EvaluateBaccarat(houseHand) % 10;
            }

            else if (playerHand.Count() > 2)
            {
                int rule = playerHand[3];
                
                if (houseSum <= 2)
                {
                    houseHand = Hit(houseHand);
                    
                }
                else if(houseSum == 3 && rule !=8)
                {
                    houseHand = Hit(houseHand);
                }
                else if (houseSum == 4 && Enumerable.Range(2, 7).Contains(rule))
                {
                    houseHand = Hit(houseHand);
                }
                else if (houseSum == 5 && Enumerable.Range(4, 7).Contains(rule))
                {
                    houseHand = Hit(houseHand);
                }
                else if (houseSum ==6 && Enumerable.Range(6, 7).Contains(rule))
                {
                    houseHand = Hit(houseHand);
                }

                houseSum = EvaluateBaccarat(houseHand) % 10;
            }

            //Winner Winner Chicken Dinner
            win = Winner(houseSum, playerSum);

            //Odds
            if (win == "Player") { payout = 1; }
            else if (win == "Tie") { payout = 8; }
            else { payout = 0; }

            return payout;
        }

        private int EvaluateBlackjack(List<int> hand)
        {

            return sum;
        }

        public float Blackjack()
        {
            payout = 0;
            return payout;
        }
      
        public float Craps()
        {
            payout = 0;
            return payout;
        }

        public float Roulette()
        {
            payout = 0;
            return payout;
        }

        public float Keno()
        {
            payout = 0;
            return payout;
        }
        //        case "craps":
        //            //Need logic passed from player/bet to bool
        //            bool Pass = true;
        //            int roll = 1;
        //            //Pass or Don't Pass
        //            List<int> dice = new List<int>();
        //            Random r = new Random();
        //            int dice1 = r.Next(1, 6);
        //            int dice2 = r.Next(1, 6);

        //            dice.Add(dice1);
        //            dice.Add(dice2);

        //            string diceroll = dice.Sum().ToString();
        //            //!st Roll
        //            if (Pass)
        //            {
        //                if (roll == 1)
        //                {
        //                    switch (int.Parse(diceroll))
        //                    {
        //                        case 2:
        //                            HouseWins = true;
        //                            break;
        //                        case 3:
        //                            HouseWins = true;
        //                            break;
        //                        case 7:
        //                            HouseWins = false;
        //                            break;
        //                        case 11:
        //                            HouseWins = false;
        //                            break;
        //                        case 12:
        //                            HouseWins = true;
        //                            break;
        //                        default:
        //                            int point = int.Parse(diceroll);
        //                            HouseWins = false;
        //                            break;
        //                    }
        //                    roll += 1;
        //                }
        //            }
        //            //Pass 7 or 11 = win

        //            //Pass 2, 3, 12 = Lose

        //            //Point = roll outcome
        //            //Roll until Point or 7
        //            //If point then win
        //            //if 7 then lose




        //            break;
    }
}
