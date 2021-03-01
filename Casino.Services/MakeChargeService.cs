using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Casino.Data;
using Casino.Models;
using Casino.WebApi.Models;
using Stripe;
namespace Casino.Services
{

    public class MakeChargeService
    {
        private readonly Guid _playerGuid;
        //private ApplicationDbContext _db = new ApplicationDbContext();

        

        public MakeChargeService()
        {

        }
        public MakeChargeService(Guid userGuid)
        {
            _playerGuid = userGuid;
        }


        //Create - return bool to use in if/else endpoint
        public bool CreateChargeforChips(RevisedChargeModel chargeModel)
        {
            // make a charge


            // use charge method

            //// if successful, make entry into charge table
          
                var entityChips = new ChargeForChips()
                {
                    PlayerId = _playerGuid,
                    ChargeTime = DateTimeOffset.Now,
                    ChargeAmount = chargeModel.Value
                };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.ChargesForChips.Add(entityChips);
                    // add entry to charges and if successful add entry to bank table
                    if (ctx.SaveChanges() != 0)
                    {
                        var bankModel = new BankTransactionCreate();
                        bankModel.PlayerId = entityChips.PlayerId;
                        bankModel.BankTransactionAmount = entityChips.ChargeAmount;

                    
                        var bankService = new BankTransactionService();
                        if (!bankService.CreateBankTransaction(bankModel))
                            { return false; }
                        return true;
                    }

                    return false;
                }
            

        }

        //then make entry into bank transaction at endpoint



        public static async Task<dynamic> ChargeAsync(string cardNumber, long month, long year, string cvc, string zip, int value)
        {
            try
            {
                // use secret (test) key 
                StripeConfiguration.ApiKey = "sk_test_51IPcYzEaVFltQHPezdflBTQkF7dWeii1TG5Du6Cvgc95VETYsz1VC0YzAmG2u" +
                    "XVoVIfHLypXdm8ghoqwgS0BLvfn00ZSfKjjZG";

                // capture card ( card is captured, sent to stripe, returned as token to charger to run payment

                var optionsToken = new TokenCreateOptions
                {
                    // TokenCardOptions changed from CreditCardOptions

                    Card = new TokenCardOptions
                    {

                        Number = cardNumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = cvc,
                        AddressZip = zip

                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionsToken);


                var options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "usd",
                    Description = "test", // put playerId 
                    Source = stripeToken.Id // generated token's id from the captured card

                };

                // make the charge
                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Paid)
                {
                    //ctx =>  add amount to bank transaction for player or move charge to bank transaction
                    //var entity = new ChargeForChips()
                    //{
                    //    PlayerId = _playerGuid,
                    //    BankTransactionAmount = model.BankTransactionAmount,
                    //    DateTimeOfTransaction = DateTimeOffset.Now

                    //};

                    return ($"Card successfully charged ${charge.Amount / 100}");
                }

            }
            catch (Exception error)
            {

                return error.Message;
            }

            // return null to finish off code paths - should be changed to something better???
            return null;
        }

        // standard not-async method




        public static bool Charge(string cardNumber, long month, long year, string cvc, string zip, int value)
        {
            try
            {
                // use secret (test) key 
                StripeConfiguration.ApiKey = "sk_test_51IPcYzEaVFltQHPezdflBTQkF7dWeii1TG5Du6Cvgc95VETYsz1VC0YzAmG2u" +
                    "XVoVIfHLypXdm8ghoqwgS0BLvfn00ZSfKjjZG";

                // capture card ( card is captured, sent to stripe, returned as token to charger to run payment

                var optionsToken = new TokenCreateOptions
                {
                    // TokenCardOptions changed from CreditCardOptions

                    Card = new TokenCardOptions
                    {
                        Number = cardNumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = cvc,
                        AddressZip = zip
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = serviceToken.Create(optionsToken);


                var options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "usd",
                    Description = "test", // put playerId 
                    Source = stripeToken.Id // generated token's id from the captured card

                };

                // make the charge
                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.Paid)
                {

                    //return ($"Card successfully charged ${ (charge.Amount/100)}");
                    return (true);
                }
                else { return false; };

            }
            catch (Exception error)
            {

                return (false);
            }

            // return null to finish off code paths - should be changed to something better???
            //return null;
        }


    }
}






