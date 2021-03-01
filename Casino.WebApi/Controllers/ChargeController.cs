using Casino.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Casino.Services;
using Microsoft.AspNet.Identity;

namespace Casino.WebApi.Controllers
{
    [Authorize]
    public class ChargeController : ApiController
    {
        private MakeChargeService CreateMakeChargeServiceForGuid()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var makeChargeService = new MakeChargeService(userId);
            return makeChargeService;
        }


        [Route("pay")]
        public async Task<dynamic> Pay(RevisedChargeModel charge)
        {
            return await MakeChargeService.ChargeAsync(charge.CardNumber, charge.Month, charge.Year, charge.Cvc, charge.Zip, charge.Value);
        }


        [Route("paySync")]
        public IHttpActionResult Charge(RevisedChargeModel charge)
        {
            var newCharge = MakeChargeService.Charge(charge.CardNumber, charge.Month, charge.Year, charge.Cvc, charge.Zip, charge.Value);
            if (newCharge == true)
            {
                MakeChargeService chargeService = CreateMakeChargeServiceForGuid();
                // adds entry to Charge for Chips table AND Bank transaction Table AND Player Balance
                chargeService.CreateChargeforChips(charge);
                return Ok("charge made - check tables for test"); // put in message
            }
            else
                return InternalServerError(); // put in message




        }

        //GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
