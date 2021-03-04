using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
