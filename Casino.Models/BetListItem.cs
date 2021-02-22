using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class BetListItem
    {
     
        public int BetId { get; set; }

        public Guid PlayerId { get; set; }
      
        public int GameId { get; set; }
      
        public double BetAmount { get; set; }

        public double PayoutAmount { get; set; } //Positive for win, Negative for loss
        public bool PlayerWonGame { get; set; }


        public DateTimeOffset DateTimeOfBet { get; set; }

    }
}
