﻿using System;
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
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        //add range to GameId FK to ensure they choose a valid game
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        [Required]
        public double BetAmount { get; set; }
        public double PayoutAmount { get; set; } //Positive for win, Negative for loss

        public bool PlayerWonGame //putting simple logic here not working
        { get;set;
            //get
            //{
            //    return PayoutAmount > 0.0;
            //}
        }


        public DateTimeOffset DateTimeOfBet { get; set; }





    }
}
