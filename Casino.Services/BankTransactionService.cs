using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Services
{
    public class BankTransactionService
    {
        private readonly Guid _playerGuid;
        private readonly int _playerId;

        public BankTransactionService(Guid userGuid)
        {
            _playerGuid = userGuid;
        }
        public bool CreateTransaction(BankTransactionCreate model)
        {
            var entity = new BankTransaction()
            {
                PlayerId = model.PlayerId,
                BankTransactionAmount = model.BankTransactionAmount,
                DateTimeOfTransaction = DateTimeOffset.Now

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.BankTransactions.Add(entity);
                return ctx.SaveChanges() != 0;
            }
        }
        public IEnumerable<BankTransactionListItem> PlayerGetBankTransactions()//PlayerGetBets(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BankTransactions
                        .Where(e => e.PlayerId == _playerId)
                                                .Select(
                            e =>
                                new BankTransactionListItem
                                {

                                    PlayerId = e.PlayerId,
                                    DateTimeOfTransaction = e.DateTimeOfTransaction,
                                    BankTransactionAmount = e.BankTransactionAmount,
                                    BankTransactionId = e.BankTransactionId,
                                }
                        );

                return query.ToArray();
            }
        }

        public BankTransactionListItem GetBankTransactionById(int id) //if this looks identical to BetListItem we can call that model instead of having 2
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BankTransactions
                        .Single(e => e.PlayerId == _playerId && e.BankTransactionId == id);
                return
                    new BankTransactionListItem

                    {
                        PlayerId = entity.PlayerId,
                        DateTimeOfTransaction = entity.DateTimeOfTransaction,
                        BankTransactionAmount = entity.BankTransactionAmount,
                        BankTransactionId = entity.BankTransactionId,

                    };

            }
        }
    }
}
