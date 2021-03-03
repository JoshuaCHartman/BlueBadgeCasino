using System;

namespace Casino.Models
{
    public class PlayerListItem
    {
        public Guid PlayerId { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public string PlayerEmail { get; set; }
        public bool IsActive { get; set; }
        public double CurrentBankBalance { get; set; }
  
    }
}
