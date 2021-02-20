using Casino.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
