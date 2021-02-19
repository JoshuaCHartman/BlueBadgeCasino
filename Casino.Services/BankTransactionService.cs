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
        public bool CreateBankTransaction(BankTransactionCreate model)
        {//maybe return the model and player balance instead of bool
            var entity = new BankTransaction()
            {
                PlayerId = _playerGuid,
                BankTransactionAmount = model.BankTransactionAmount,
                DateTimeOfTransaction = DateTimeOffset.Now

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.BankTransactions.Add(entity);
                if (ctx.SaveChanges() != 0 && UpdatePlayerBankBalance(_playerGuid, model.BankTransactionAmount))
            { return true; }
                return false; }
            
        }
        public IEnumerable<BankTransactionListItem> PlayerGetBankTransactions()//PlayerGetBets(int playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BankTransactions
                        .Where(e => e.PlayerId == _playerGuid)
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
                        .Single(e => e.PlayerId == _playerGuid && e.BankTransactionId == id);
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
        //same method that is in BetService CreateBet
        private bool UpdatePlayerBankBalance(Guid playerId, double amount)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                       .Players
                       .Single(e => e.PlayerId == playerId);
                entity.BankBalance = entity.BankBalance + amount; 
                return entity.SaveChanges() == 1;                
            }
        }
    }
}
