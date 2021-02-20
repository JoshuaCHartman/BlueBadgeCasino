using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Data
{
    //different models for CRUD operations
    //Models represent class object presented to user for manupilation
    public enum GameType
    {
        Cards, Dice, Random_Num, Wheel
    }
    
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public GameType TypeOfGame { get; set; }
        public bool IsHighStakes { get; set; }
        public double MinBet { get; set; }
        public double MaxBet { get; set; }
        
    }



}
