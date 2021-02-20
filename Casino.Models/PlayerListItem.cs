using Casino.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class PlayerListItem
    {
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public string PlayerPhone { get; set; }
        public string PlayerEmail { get; set; }
        public string PlayerAddress { get; set; }
        public PlayerState PlayerState { get; set; }
        public DateTime PlayerDoB { get; set; }
        public DateTime AccountCreated { get; set; }
        public bool IsActive { get; set; }
        public double CurrentBankBalance { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
