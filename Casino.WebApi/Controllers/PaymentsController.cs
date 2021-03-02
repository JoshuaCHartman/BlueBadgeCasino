using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Delete!! 03/02 jch
// DELETE IF NOT USING NEW IMPLEMENTATION OF STRIPE 

namespace Casino.WebApi.Controllers
{
    public class PaymentsController : Controller
    {
        
            // GET: Payments
            public ActionResult Index()
            {
                return View();
            }


            public PaymentsController()
            {
                StripeConfiguration.ApiKey = "sk_test_51IPcYzEaVFltQHPezdflBTQkF7dWeii1TG5Du6Cvgc95VETYsz1VC0YzAmG2uXVoVIfHLypXdm8ghoqwgS0BLvfn00ZSfKjjZG";

            }

            [HttpPost]
            public ActionResult CreateCheckoutSession()
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
        {
          "card",
        },
                    LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },

            },
            Quantity = 1,
          },
        },
                    Mode = "payment",
                    SuccessUrl = "https://example.com/success",
                    CancelUrl = "https://example.com/cancel",
                };

                var service = new SessionService();
                Session session = service.Create(options);

                return Json(new { id = session.Id });
            }

        }
    }
