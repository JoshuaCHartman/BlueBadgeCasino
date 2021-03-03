
namespace Casino.Models
{
    public class BetDetail
    {
        public int BetId { get; set; }

        public int GameId { get; set; }
        
        public double BetAmount { get; set; }

        public bool PlayerWonGame { get; set; }

        public double PayoutAmount { get; set; } //Positive for win, Negative for loss

        public string TimeOfBet { get; set; }
        
    }
}
