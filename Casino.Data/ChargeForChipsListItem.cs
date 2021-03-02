using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
