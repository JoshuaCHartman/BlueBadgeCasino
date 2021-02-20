using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class BankTransactionCreate
    {
        [Required]
        public int PlayerId { get; set; }

        [Required]
        public double BankTransactionAmount { get; set; }
    }
}
