using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Casino.WebApi.Models
{
    public class RevisedChargeModel
    {
        public Guid PlayerId { get; set; }
        public string CardNumber { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }
        public string Cvc { get; set; }
        public string Zip { get; set; }
        public int Value { get; set; }
    }
}