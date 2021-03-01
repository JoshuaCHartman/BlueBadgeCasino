using Casino.Data;
using Casino.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class GameSimulation
    {
     
        
        private readonly Random _random = new Random();
        private Game _game = new Game();
        public double PlayGame(double betAmount, int gameId)
        {
           
            //Game game = GetGameById(gameId);
            //if (betAmount < game.MinBet || betAmount > game.MaxBet)
            //    return 0;
            int x = _random.Next(10);
            if (x <= 3)  //this is 60/40 odds
                return betAmount; //player wins, wins bet amount

            return  (-1)*betAmount; //player loses, payout is negative the bet amount
        }

        // helper method to convert gamesim's payout to a bool if payout is +
        //This is being called from BetService
        public bool GameWinOutcome(double payout)
        {
            bool wonGame;
            if (payout > 0) { wonGame = true; } else { wonGame = false; }
            return wonGame;
        }



        //Jon's method below
        public double PlayGame(int id, double betAmt)
        {
            double amount = 0;
            var game = new GameService();
            string gameName = game.GetGameById(id).GameName;
            double payout = 0;
            switch (gameName.ToLower())
            {
                case "baccarat":
                    game.Baccarat();
                    break;
                case "blackjack":
                    game.Blackjack();
                    break;
                case "craps":
                    //bool Pass or Don't Pass bet type
                    //Not quite there yet
                    bool pass = true;
                    game.Craps(pass);
                    break;
                case "roulette":
                    //bet types
                    //Straight 1 number
                    //Split 2 numbers adjoining
                    //Street 1 row
                    //Corner 4 numbers adjoining
                    //Double Street 2 rows
                    //Trio 0,1,2 or 00,2,3 (00 = 37 for sanity)
                    //Basket 0,00,1,2,3 (00 = 37 for sanity)
                    //Low < 19
                    //High > 18
                    //Red
                    //Black
                    //Even 
                    //Odd
                    //Dozen 1-12; 13-24; 25-36
                    //Column mod 3 = 1, 2, or 3
                    //Snake 1, 5, 9, 12, 14, 16, 19, 23, 27, 30, 32, 34
                    game.baseGame();
                    break;
                case "keno":
                    //List<int> from "player selection" range = 1-80; up to 20 #'s selected - let's use 10 #'s
                    game.baseGame();
                    break;
                case "russian roulette":
                    game.RussianRoulette();
                    break;
                default:
                    break;
            }
            if (payout > 0) { amount = payout * betAmt + betAmt; }
            else if (payout < 0) { }//accountDelete
            else { amount = -1 * betAmt; }
            return amount;
        }




    }





    //Helper Method
    //public Game GetGameById(int gameId)
    //{
    //    //just like gold badge repo
          //Either go get a game object or a gameModel
    //    //logic here or change get it straight from GameService GetGame method
    //}


}
