using Casino.Data;
using System;
using System.ComponentModel.DataAnnotations;


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
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string PlayerEmail { get; set; }
        public string PlayerAddress { get; set; }

        public PlayerState PlayerState { get; set; }

        public string PlayerZipCode { get; set; }

        [Required]

        public string PlayerDob { get; set; }

        public TierStatus TierStatus { get; set; } = TierStatus.bronze;

        public DateTimeOffset AccountCreated { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
