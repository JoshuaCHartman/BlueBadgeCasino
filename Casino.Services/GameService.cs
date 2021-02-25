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
        public double payout = 0;
        private readonly Guid _userId;
        private int houseSum = 0;
        private int playerSum = 0;

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

        private List<int> Roll(int numberOfDice)
        {
            int i;
            int dice = numberOfDice;
            List<int> roll = new List<int>();
            for (i = 1; i < dice + 1; i++)
            {
                int diceRoll = r.Next(1, 6);
                roll.Add(diceRoll);
            }
            return roll;
        }

        private int Spin(int chances)
        {
            int spin = r.Next(1, chances);

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

        public double Baccarat()

        {
            List<int> houseHand = Deal(2);
            List<int> playerHand = Deal(2);
            string win = "";
            //private method to eval hand(s)

            //Value houseHand
            houseSum = EvaluateBaccarat(houseHand) % 10;

            //Value playerHand
            playerSum = EvaluateBaccarat(playerHand) % 10;

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
            else if (playerHand.Count() == 2)
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
                else if (houseSum == 3 && rule != 8)
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
                else if (houseSum == 6 && Enumerable.Range(6, 7).Contains(rule))
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
            sum = 0;
            foreach (int c in hand)
            {
                int card = hand[c];
                hand.Remove(card);
                if (card > 10) { card = 10; }
                hand.Add(card);
            }

            sum = hand.Sum();

            return sum;
        }

        private int EvaluateAces(List<int> hand)
        {
            sum = 0;


            sum = hand.Sum();

            return sum;
        }

        public double Blackjack()
        {
            List<int> houseHand = Deal(2);
            List<int> playerHand = Deal(2);

            playerSum = EvaluateBlackjack(playerHand);
            houseSum = EvaluateBlackjack(houseHand);

            //player = dealer == push (draw) no winner
            if (playerSum == houseSum) { payout = 0; }
            //player > 21 = bust
            else if (playerSum > 21) { payout = 0; }
            //dealer bust = player win
            else if (playerSum < 21 && houseSum > 21) { payout = 1; }
            //player > house = player win
            else if (playerSum <= 21 && playerSum > houseSum) { payout = 1; }
            //player gets 10 + ace = win
            else if (playerHand.Contains(1) && playerHand.Contains(10)) { payout = 1.5; }

            return payout;
        }

        public double Craps(bool Pass) //Pass or Don't Pass bet
        {
            bool pass = Pass;
            int point = 0;
            sum = Roll(2).Sum();
            int round = 1;
            //!st Roll
            if (Pass)
            {
                if (round == 1)
                {
                    switch (sum)
                    {
                        case 2:
                            payout = 0;
                            break;
                        case 3:
                            payout = 0;
                            break;
                        case 7:
                            payout = 1;
                            break;
                        case 11:
                            payout = 1;
                            break;
                        case 12:
                            payout = 0;
                            break;
                        default:
                             point = sum;
                            break;
                    }
                    round += 1;
                }
            }
            
            while (sum != point || sum != 7)
            {
                sum = Roll(2).Sum();

                if (sum == point) { payout = 1; break; }
                else if (sum == 7) { payout = 0; break; }
            }

            return payout;
        }

        private Dictionary<string, int> RouletteWheel()
        {
            Dictionary<string, int> wheel = new Dictionary<string, int>();
            wheel.Add("Green", 0);
            wheel.Add("Black", 28);
            wheel.Add("Red", 9);
            wheel.Add("Black", 26);
            wheel.Add("Black", 30);
            wheel.Add("Black", 11);
            wheel.Add("Red", 7);
            wheel.Add("Black", 20);
            wheel.Add("Black", 32);
            wheel.Add("Black", 17);
            wheel.Add("Red", 5);
            wheel.Add("Black", 22);
            wheel.Add("Black", 34);
            wheel.Add("Black", 15);
            wheel.Add("Red", 3);
            wheel.Add("Black", 24);
            wheel.Add("Black", 36);
            wheel.Add("Black", 13);
            wheel.Add("Red", 1);
            wheel.Add("Green", 0);
            wheel.Add("Red", 27);
            wheel.Add("Black", 10);
            wheel.Add("Red", 25);
            wheel.Add("Black", 29);
            wheel.Add("Black", 12);
            wheel.Add("Black", 8);
            wheel.Add("Red", 19);
            wheel.Add("Black", 31);
            wheel.Add("Black", 18);
            wheel.Add("Black", 6);
            wheel.Add("Red", 21);
            wheel.Add("Black", 33);
            wheel.Add("Black", 16);
            wheel.Add("Black", 4);
            wheel.Add("Red", 23);
            wheel.Add("Black", 35);
            wheel.Add("Black", 14);
            wheel.Add("Black", 2);

            return wheel;
        }

        public double Roulette()
        {
            Dictionary<string, int> rouletteWheel = RouletteWheel();



            payout = 0;
            return payout;
        }

        public double Keno(List<int> playerNumbers)
        {
            List<int> drawingNumbers = new List<int>();


            payout = 0;
            return payout;
        }
    }
}
