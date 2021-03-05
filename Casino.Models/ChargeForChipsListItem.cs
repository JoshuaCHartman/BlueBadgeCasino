using System;


namespace Casino.Data
{
    public class ChargeForChipsListItem
    {
        
        public int ChargeId { get; set; }
        public Guid PlayerId { get; set; }
        public DateTimeOffset ChargeTime { get; set; }
        public double ChargeAmount { get; set; } //positive for deposit, negative for withdraw

    }
}
