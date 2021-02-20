using Casino.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        [Display(Name = "Birthday: Enter in format MMDDYYY (example : 10312021")]
        [Required]
        public string PlayerDob { get; set; }
        public TierStatus TierStatus { get; set; }
        public bool IsActive { get; set; }
        //public bool HasAccessToHighLevelGame { get; set; }
        //public double CurrentBankBalance { get; set; }
        //public bool EligibleForReward { get; set; }
    }
}
