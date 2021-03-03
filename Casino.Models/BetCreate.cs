
using System.ComponentModel.DataAnnotations;

namespace Casino.Models
{
    public class BetCreate
    {
        
        [Required]
        public int GameId { get; set; }

        [Required]
        public double BetAmount { get; set; }
        public double PayoutAmount { get; set; }
        public bool PlayerWonGame
        {
            get; set;
        }

    }
}
