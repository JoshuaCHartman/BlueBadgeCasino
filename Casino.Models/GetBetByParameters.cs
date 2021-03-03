using System;

namespace Casino.Models
{
   public class GetBetByParameters
    {
        public Guid? PlayerId { get; set; }

        public int? GameId { get; set; }

        public bool? PlayerWonGame { get; set; }
        public int? Time { get; set; }
    }
}
