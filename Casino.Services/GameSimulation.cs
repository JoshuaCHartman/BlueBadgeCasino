using Casino.Data;
using System;

namespace Casino.Models
{
    public class GameSimulation
    {

        private readonly Random _random = new Random();
        private Game _game = new Game();
        public double PlayGame(double betAmount, int gameId)
        {
            int x = _random.Next(10);
            if (x <= 3)  //this is 60/40 odds
                return betAmount; //player wins, wins bet amount

            return (-1) * betAmount; //player loses, payout is negative the bet amount
        }

        // helper method to convert gamesim's payout to a bool if payout is +
        //This is being called from BetService
        public bool GameWinOutcome(double payout)
        {
            bool wonGame;
            if (payout > 0) { wonGame = true; } else { wonGame = false; }
            return wonGame;
        }
    }
}