using Casino.Data;

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
