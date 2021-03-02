using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Casino.WebApi.Models
{
    // model to return the data from charge
    public class ChargeViewModel
    {
        public string ChargeId { get; set; }

        public long ChargeAmount { get; set; }



    }
}