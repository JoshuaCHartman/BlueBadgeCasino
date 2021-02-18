using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Data
{
    public class BankTransactionListItem
    {
        public int BankTransactionId { get; set; }

        public int PlayerId { get; set; }
      
        public DateTimeOffset DateTimeOfTransaction { get; set; }

        public double BankTransactionAmount { get; set; } //positive for deposit, negative for withdraw
    }
}
