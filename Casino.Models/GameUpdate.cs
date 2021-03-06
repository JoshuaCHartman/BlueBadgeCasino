using System.ComponentModel.DataAnnotations;

namespace Casino.Models
{
    public class GameUpdate
    {
        [Required]
        public int GameId { get; set; }
        [Required]
        public double MinBet { get; set; }
        public double MaxBet { get; set; }
    }
}
