using Casino.WebApi.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// front end form submit card - controller

namespace Casino.WebApi.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken)
        {
            // TryCatch block for error handling - removed until basic functions built out

            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}

            // Move key to web.config
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
            Charge charge =service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            model.ChargeAmount = charge.Amount;


            return View("PurchaseStatus", model);
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
