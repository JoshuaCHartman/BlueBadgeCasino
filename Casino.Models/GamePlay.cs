using Casino.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class GamePlay
    {
        public string GameName { get; set; }
        public GameType TypeOfGame { get; set; }
        public bool IsHighStakes { get; set; }
        public double MinBet { get; set; }
        public double MaxBet { get; set; }
       
    }
}
