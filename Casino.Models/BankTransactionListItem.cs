using System;

namespace Casino.Data
{
    public class BankTransactionListItem
    {
        public int BankTransactionId { get; set; }

        public Guid PlayerId { get; set; }

        public DateTimeOffset DateTimeOfTransaction { get; set; }

        public double BankTransactionAmount { get; set; } //positive for deposit, negative for withdraw
    }
}
