using System;
using System.ComponentModel.DataAnnotations;


namespace Casino.Models
{
    public class BankTransactionCreate
    {
        public Guid PlayerId { get; set; }

        [Required]
        public double BankTransactionAmount { get; set; }
    }
}
