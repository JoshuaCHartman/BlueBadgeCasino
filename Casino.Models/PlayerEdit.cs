using Casino.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Casino.Models
{
    public class PlayerEdit
    {
        public Guid PlayerId { get; set; }

        public string PlayerPhone { get; set; }

        public string PlayerAddress { get; set; }
        public PlayerState PlayerState { get; set; }

        public string PlayerZipCode { get; set; }
        
      
    }
}
