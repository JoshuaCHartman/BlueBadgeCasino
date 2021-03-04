using Casino.Data;
using System.ComponentModel.DataAnnotations;

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

        public string PlayerZipCode { get; set; }
        
        [Display(Name = "Birthday: Enter in format MMDDYYY (example : 10312021")]
        [Required]
        //public DateTime PlayerDob { get; set; }
        public string PlayerDob { get; set; }
        public TierStatus TierStatus { get; set; } = TierStatus.bronze;
        public bool IsActive { get; set; }
        //public bool HasAccessToHighLevelGame { get; set; }
        //public double CurrentBankBalance { get; set; }
        //public bool EligibleForReward { get; set; }
    }
}
