using Casino.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class GameListItem
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public GameType TypeOfGame { get; set; }
        public double MinBet { get; set; }
        public double MaxBet { get; set; }

    }
}
