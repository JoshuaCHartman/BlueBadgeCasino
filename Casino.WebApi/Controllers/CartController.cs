using Casino.WebApi.Models;
using Stripe;
using System.Web.Mvc;

// front end form submit card - controller

namespace Casino.WebApi.Controllers
{
    public class CartController : Controller
    {

        // //GET and POST both required for cart to function - GET the view, then POST the charge...

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51IPcYzEaVFltQHPezdflBTQkF7dWeii1TG5Du6Cvgc95VETYsz1VC0YzAmG2uXVoVIfHLypXdm8ghoqwgS0BLvfn00ZSfKjjZG";

            // `source` is obtained with Stripe.js; see https://stripe.com/docs/payments/accept-a-payment-charges#web-create-token
            var options = new ChargeCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                //Source = "tok_amex",
                Source = stripeToken,
                Description = "My First Test Charge (created for API docs)",
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            model.ChargeAmount = charge.Amount;


            return View("PurchaseStatus", model);
        }
    }
}
