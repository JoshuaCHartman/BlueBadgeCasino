using Casino.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Models
{
    public class PlayerEdit
    {
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public string PlayerPhone { get; set; }
        public string PlayerEmail { get; set; }
        public string PlayerAddress { get; set; }
        public PlayerState PlayerState { get; set; }
        public DateTime PlayerDoB { get; set; }
        public TierStatus TierStatus { get; set; }
        public bool IsActive { get; set; }
        public bool HasAccessToHighLevelGame { get; set; }
        public double CurrentBankBalance { get; set; }
        public bool EligibleForReward { get; set; }
    }
}
