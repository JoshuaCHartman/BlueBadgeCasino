using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Data
{
    public class ChargeForChips
    {
        [Key]
        public int ChargeId { get; set; }

        [ForeignKey(nameof(Player))]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public DateTimeOffset ChargeTime { get; set; }

        public double ChargeAmount { get; set; } //positive for deposit, negative for withdraw

    }
}
