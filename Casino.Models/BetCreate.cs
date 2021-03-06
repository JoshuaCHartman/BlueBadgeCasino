
using PinnacleWrapper.Enums;
using System.ComponentModel.DataAnnotations;

namespace Casino.Models
{
    public enum BetType
    {
        basket,
        black,
        column,
        corner,
        double_street,
        dozen,
        even,
        high,
        low,
        no_pass,
        odd,
        pass,
        red,
        snake,
        split,
        straight,
        street,
        trio
    }

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
        public BetType? TypeOfBet { get; set; }
        public string ValueOfBet { get; set; } = "0";

    }
}