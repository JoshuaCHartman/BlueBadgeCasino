using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Data
{
    public class BankTransaction
    {
        [Key]
        public int BankTransId { get; set; }

        [ForeignKey(nameof(Player))]
        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public DateTimeOffset DateTimeOfTrans { get; set; }

        public double BankTransAmount { get; set; } //positive for deposit, negative for withdraw


    }
}
