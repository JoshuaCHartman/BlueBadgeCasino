using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class BetCreate
    {
        //[Required]
        //public Guid PlayerId { get; set; }// this can be removed from here if we build it into service layer

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
