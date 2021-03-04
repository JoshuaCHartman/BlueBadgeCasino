using Casino.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Casino.Models 
{

    public class PlayerCreate
    {
        [Required]
        public string PlayerFirstName { get; set; }
        [Required]
        public string PlayerLastName { get; set; }
        public string PlayerPhone { get; set; }
        [Required]
        public string PlayerEmail { get; set; }
        public string PlayerAddress { get; set; }

        public PlayerState PlayerState { get; set; }

        [Required]
        [Display(Name = "Birthday: Enter in format MMDDYYY (example : 10312021")]
        //public DateTime PlayerDob { get; set; }
        public string PlayerDob { get; set; }

        public DateTimeOffset AccountCreated { get; set; }
        public bool IsActive { get; set; } = true;

        //public bool IsActive { get; set; }

        //public bool HasAccessToHighLevelGame { get; set; }

        //public double CurrentBankBalance { get; set; }

        // public virtual List<BankTransaction> BankTransactions { get; set; }

        //public virtual List<Bet> Bets { get; set; }


        //public bool EligibleForReward { get; set; }


        //public bool AgeVerification { get; set; }


        //public DateTimeOffset CreatedUtc { get; set; }

        //public DateTimeOffset? ModifiedUtc { get; set; }
    }
        }
