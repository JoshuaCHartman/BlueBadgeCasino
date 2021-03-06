using Casino.Data;
using System.ComponentModel.DataAnnotations;

namespace Casino.Models
{
    public class GameCreate
    {
        [Required]
        public string GameName { get; set; }
        [Required]
        public GameType TypeOfGame { get; set; }
        public double MinBet { get; set; }
        public double MaxBet { get; set; }
    }
}
