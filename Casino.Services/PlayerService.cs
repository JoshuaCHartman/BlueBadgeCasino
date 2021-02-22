using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.Services
{
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity = new Player()
            {
                PlayerId = _userId,
                PlayerFirstName = model.PlayerFirstName,
                PlayerLastName = model.PlayerLastName,
                PlayerPhone = model.PlayerPhone,
                PlayerEmail = model.PlayerEmail,
                PlayerAddress = model.PlayerAddress, //Evaluate in testing whether its null or doesn't work
                PlayerState = model.PlayerState,
                PlayerDob = model.PlayerDob,
                AccountCreated = DateTimeOffset.Now,
                //IsActive = model.IsActive,
                //TierStatus = model.TierStatus,
                //HasAccessToHighLevelGame = model.HasAccessToHighLevelGame,
                //CurrentBankBalance = model.CurrentBankBalance,
                //EligibleForReward = model.EligibleForReward,
                //AgeVerification = model.AgeVerification,
                //CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PlayerListItem> GetPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Where(e => e.PlayerFirstName.Length > 0)
                        .Select(
                            e =>
                                new PlayerListItem
                                {
                                    PlayerFirstName = e.PlayerFirstName,
                                    PlayerLastName = e.PlayerLastName,
                                    PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    PlayerAddress = e.PlayerAddress,
                                    PlayerState = e.PlayerState,
                                    PlayerDob = e.PlayerDob,
                                    AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public PlayerDetail GetPlayerById(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == id);
                return
                    new PlayerDetail
                    {
                        PlayerFirstName = entity.PlayerFirstName,
                        PlayerLastName = entity.PlayerLastName,
                        PlayerPhone = entity.PlayerPhone,
                        PlayerEmail = entity.PlayerEmail,
                        PlayerAddress = entity.PlayerAddress,
                        PlayerState = entity.PlayerState,
                        PlayerDob = entity.PlayerDob,
                        AccountCreated = entity.AccountCreated,
                        IsActive = entity.IsActive,
                        CurrentBankBalance = entity.CurrentBankBalance,
                        //CreatedUtc = entity.CreatedUtc,
                        //ModifiedUtc = entity.ModifiedUtc
                    };
            }

        }

        public IEnumerable<PlayerListItem> GetPlayerByTierStatus(TierStatus TierStatus)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Where(e => e.TierStatus == TierStatus)
                        .Select(
                            e =>
                                new PlayerListItem
                                {
                                    PlayerFirstName = e.PlayerFirstName,
                                    PlayerLastName = e.PlayerLastName,
                                    PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    PlayerAddress = e.PlayerAddress,
                                    PlayerState = e.PlayerState,
                                    PlayerDob = e.PlayerDob,
                                    AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<PlayerListItem> GetPlayerByBalance(double CurrentBankBalance)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Where(e => e.CurrentBankBalance > 0)
                        .Select(
                            e =>
                                new PlayerListItem
                                {
                                    PlayerFirstName = e.PlayerFirstName,
                                    PlayerLastName = e.PlayerLastName,
                                    PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    PlayerAddress = e.PlayerAddress,
                                    PlayerState = e.PlayerState,
                                    PlayerDob = e.PlayerDob,
                                    AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public IEnumerable<PlayerListItem> GetActivePlayers(bool IsActive)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Players
                        .Where(e => e.IsActive == true)
                        .Select(
                            e =>
                                new PlayerListItem
                                {
                                    PlayerFirstName = e.PlayerFirstName,
                                    PlayerLastName = e.PlayerLastName,
                                    PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    PlayerAddress = e.PlayerAddress,
                                    PlayerState = e.PlayerState,
                                    PlayerDob = e.PlayerDob,
                                    AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }

        public bool UpdatePlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == _userId);

                entity.PlayerPhone = model.PlayerPhone;
                entity.PlayerAddress = model.PlayerAddress;
                entity.PlayerState = model.PlayerState;
                //entity.PlayerDob = model.PlayerDob;
                //entity.TierStatus = model.TierStatus;
                //entity.IsActive = model.IsActive;
                //entity.HasAccessToHighLevelGame = model.HasAccessToHighLevelGame;
                //entity.CurrentBankBalance = model.CurrentBankBalance;
                //entity.EligibleForReward = model.EligibleForReward;
                //entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePlayer(Guid id) //Does not actually delete
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == _userId);

                entity.IsActive = false;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}