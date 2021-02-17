using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Data
{
    public class Bet
    {
        [Key]
        public int BetId { get; set; }

        [ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Required]
        public double BetAmount { get; set; }

        public bool PlayerWonGame { get; set; }

        public double PayoutAmount { get; set; } //Positive for win, Negative for loss

        public DateTimeOffset DateTimeOfBet { get; set; }





    }
}
