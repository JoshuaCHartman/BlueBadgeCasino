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
        public PlayerService()
        {

        }
        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity = new Player()
            {
                //PlayerId = _userId,
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
        //Admin get All players
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
                                    PlayerId = e.PlayerId,
                                    PlayerFirstName = e.PlayerFirstName,
                                    PlayerLastName = e.PlayerLastName,
                                    //PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    //PlayerAddress = e.PlayerAddress,
                                    //PlayerState = e.PlayerState,
                                    //PlayerDob = e.PlayerDob,
                                    //AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }
        //Player gets own info
        public PlayerDetail GetSelf()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == _userId);
                return
                    new PlayerDetail
                    {
                        //PlayerId = entity.PlayerId, leave blank so player can't see Guid
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
                        //ModifiedUtc = entity.ModifiedUtc
                    };
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
                        PlayerId = entity.PlayerId, //Admin SHOULD see player Guid
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
        //Admin Get by TierStatus
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
                                    //PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    //PlayerAddress = e.PlayerAddress,
                                    //PlayerState = e.PlayerState,
                                    //PlayerDob = e.PlayerDob,
                                    //AccountCreated = e.AccountCreated,
                                    //IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }
        //Admin return all players with a positve balance
        public IEnumerable<PlayerListItem> GetPlayerByHasBalance()
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
                                    //PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    //PlayerAddress = e.PlayerAddress,
                                    //PlayerState = e.PlayerState,
                                    //PlayerDob = e.PlayerDob,
                                    //AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                            );

                return query.ToArray();
            }
        }
        //admin get active players
        public IEnumerable<PlayerListItem> GetActivePlayers()
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
                                    //PlayerPhone = e.PlayerPhone,
                                    PlayerEmail = e.PlayerEmail,
                                    // PlayerAddress = e.PlayerAddress,
                                    //PlayerState = e.PlayerState,
                                    // PlayerDob = e.PlayerDob,
                                    // AccountCreated = e.AccountCreated,
                                    IsActive = e.IsActive,
                                    CurrentBankBalance = e.CurrentBankBalance,
                                    //CreatedUtc = e.CreatedUtc
                                }
                            );

                return query.ToArray();
            }
        }
        //Player update basic info
        public bool UpdatePlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == _userId);

                    //PlayerFirstName = model.PlayerFirstName,
                    //PlayerLastName = model.PlayerLastName,
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

        //Admin update player
        public bool UpdatePlayerByAdmin(PlayerEdit model, Guid playerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == playerId);

                //PlayerFirstName = model.PlayerFirstName,
                //PlayerLastName = model.PlayerLastName,
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
        
        //Player deletes account(only makes it inactive)
        public bool DeletePlayer() //Does not actually delete
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