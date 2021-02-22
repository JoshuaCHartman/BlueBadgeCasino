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
        private bool value;

        [Key]
        public int BetId { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        //add range to GameId FK to ensure they choose a valid game
        [ForeignKey(nameof(Game)), Range(1, 3, ErrorMessage = "please choose a game between 1 and 3")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Required]
        public double BetAmount { get; set; }

        public bool PlayerWonGame //putting simple logic here not working
        {
            get; 
            
            
            set;
                 
        }

        public double PayoutAmount { get; set; } //Positive for win, Negative for loss

        public DateTimeOffset DateTimeOfBet { get; set; }





    }
}
