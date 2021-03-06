using Casino.Data;
using Casino.Models;
using Casino.WebApi.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //Create - return bool to use in if/else endpoint - flow to tables
        public bool CreateChargeforChips(RevisedChargeModel chargeModel)
        {
            // make a charge
            // use charge method
            //// if successful, make entry into charge table

            var entityChips = new ChargeForChips()
            {
                PlayerId = _playerGuid,
                ChargeTime = DateTimeOffset.Now,
                ChargeAmount = chargeModel.Value / 100
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
                    if (!bankService.CreateBankTransactionCharge(bankModel))
                    { return false; }
                    return true;
                }

                return false;
            }

        }

        //then make entry into bank transaction at endpoint

        // async standalone charge method 
        public static async Task<dynamic> ChargeAsync(string cardNumber, long month, long year, string cvc, string zip, int value)
        {
            try
            {
                // use secret (test) key 
                StripeConfiguration.ApiKey = "sk_test_51IPcYzEaVFltQHPezdflBTQkF7dWeii1TG5Du6Cvgc95VETYsz1VC0YzAmG2u" +
                    "XVoVIfHLypXdm8ghoqwgS0BLvfn00ZSfKjjZG";

                // capture card ( card is captured, sent to stripe, returned as token to charger to run payment

                var tokenCreateOptions = new TokenCreateOptions
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

                var tokenService = new TokenService();
                Token newStripeToken = await tokenService.CreateAsync(tokenCreateOptions);


                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "usd",
                    Description = "test", // put playerId 
                    Source = newStripeToken.Id // generated token's id from the captured card

                };

                // make the charge
                var service = new ChargeService();
                Charge charge = await service.CreateAsync(chargeOptions);

                if (charge.Paid)
                {
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

                // capture card ( card is captured, sent to stripe, returned as token to charge to run payment )

                var tokenCreateOptions = new TokenCreateOptions
                {
                    // TokenCardOptions changed from CreditCardOptions in resources

                    Card = new TokenCardOptions
                    {
                        Number = cardNumber,
                        ExpMonth = month,
                        ExpYear = year,
                        Cvc = cvc,
                        AddressZip = zip
                    }
                };

                var tokenService = new TokenService();
                Token newStripeToken = tokenService.Create(tokenCreateOptions);

                var options = new ChargeCreateOptions
                {
                    Amount = value,
                    Currency = "usd",
                    Description = "test", // put playerId 
                    Source = newStripeToken.Id // generated token's id from the captured card

                };

                // make the charge
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(options);

                if (charge.Paid)
                {
                    return (true);
                }
                else { return false; };

            }
            catch (Exception)
            {
                return (false);
            }

        }

        //Return
        public IEnumerable<ChargeForChipsListItem> PlayerGetChargeTransactions()//PlayerGetCharges(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ChargesForChips
                        .Where(e => e.PlayerId == _playerGuid)
                                                .Select(
                            e =>
                                new ChargeForChipsListItem
                                {

                                    PlayerId = e.PlayerId,
                                    ChargeTime = e.ChargeTime,
                                    ChargeAmount = e.ChargeAmount,
                                    ChargeId = e.ChargeId,
                                }
                        );

                return query.ToArray();
            }
        }

        public ChargeForChipsListItem GetChargeTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ChargesForChips
                        .Single(e => e.PlayerId == _playerGuid && e.ChargeId == id);
                return
                    new ChargeForChipsListItem

                    {
                        PlayerId = entity.PlayerId,
                        ChargeTime = entity.ChargeTime,
                        ChargeAmount = entity.ChargeAmount,
                        ChargeId = entity.ChargeId,

                    };
            }
        }

        public IEnumerable<ChargeForChipsListItem> AdminGetChargeTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ChargesForChips
                        .Where(e => e.ChargeId > -1)
                                                .Select(
                            e =>
                                new ChargeForChipsListItem
                                {

                                    PlayerId = e.PlayerId,
                                    ChargeTime = e.ChargeTime,
                                    ChargeAmount = e.ChargeAmount,
                                    ChargeId = e.ChargeId,
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<ChargeForChipsListItem> AdminGetChargeTransactions(Guid guid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ChargesForChips
                        .Where(e => e.PlayerId == guid)
                        .Select(e =>
                                new ChargeForChipsListItem
                                {

                                    PlayerId = e.PlayerId,
                                    ChargeTime = e.ChargeTime,
                                    ChargeAmount = e.ChargeAmount,
                                    ChargeId = e.ChargeId,
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
