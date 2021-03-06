using Casino.Data;
using Casino.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


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
                PlayerDob = model.PlayerDob,
                PlayerId = _userId,
                PlayerFirstName = model.PlayerFirstName,
                PlayerLastName = model.PlayerLastName,
                PlayerPhone = model.PlayerPhone,
                PlayerEmail = model.PlayerEmail,
                PlayerAddress = model.PlayerAddress, //Evaluate in testing whether its null or doesn't work
                PlayerState = model.PlayerState,
                PlayerZipCode = model.PlayerZipCode,
                TierStatus = model.TierStatus,


                //HasAccessToHighLevelGame = model.HasAccessToHighLevelGame,
                //CurrentBankBalance = model.CurrentBankBalance,
                //EligibleForReward = model.EligibleForReward,
                //AgeVerification = model.AgeVerification,
                //CreatedUtc = DateTimeOffset.Now
                IsActive = model.IsActive,
                AccountCreated = DateTimeOffset.Now
            };


            using (var ctx = new ApplicationDbContext())

            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public bool CheckActiveStatusAdmin(PlayerListItem player)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .BankTransactions
        //                .Single(e => e.PlayerId == _userId);
        //        TimeSpan LastActive = DateTime.Now - query.DateTimeOfTransaction;
        //        if (LastActive.Days < 180)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}

        //public bool CheckActiveStatus(PlayerDetail player)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //                .BankTransactions
        //                .Single(e => e.PlayerId == _userId);
        //        TimeSpan LastActive = DateTime.Now - query.DateTimeOfTransaction;
        //        if (LastActive.Days < 180)
        //        {
        //            player.IsActive = true;
        //            return ctx.SaveChanges()==1;
        //            return true;
        //        }
        //        else
        //        {
        //            player.IsActive = false;
        //            return ctx.SaveChanges() == 1;
        //            return false;
        //        }
        //    }
        //}           


        public bool CheckPlayerIdAlreadyExists()
        {
            var ctx = new ApplicationDbContext();

            using (ctx)

            {
              var query = ctx.Players
                            .Find(_userId);
                if (query != null)
                {

                    return true;
                }
                return false;

            }
        }


        public bool CheckPlayer(PlayerCreate player)
        {   //Birthdate is not entered or correctly or legal age is not acceptable
            var ctx = new ApplicationDbContext();
            if (!DateTime.TryParse(player.PlayerDob, out DateTime testDob))
               
                {
                var query = ctx.Players
                            .Find(_userId);
                if (query != null)
                {

                    return true;
                }
                return false;

            }
            else
            {

                return true;
            }
        }




        //public bool CheckPlayer(PlayerCreate player)
        //{   //Getting the string
        // var stringDob = player.PlayerDob;

        //Birthdate is not entered or correctly or legal age is not acceptable

        //    // when this was changed to a string, this always fails and we cannot create any players

        //    //if (!DateTime.TryParse(player.PlayerDob, out DateTime testDob))
        //    DateTime parsedDob;
        //    DateTime.TryParseExact(player.PlayerDob, "MMDDYYYY",
        //                   CultureInfo.CurrentCulture,
        //                   DateTimeStyles.None,
        //                   out parsedDob);

        //        if (parsedDob == null)
        //        return false;
        //    return true;


     

        public bool CheckDob(PlayerCreate player)
        {
            //Getting the string
            var stringDob = player.PlayerDob;


            //Convert the string to a DateTime
            DateTime convertedDob;

            // This only parses WITH SLASHES 
            convertedDob = DateTime.Parse(stringDob);

           
            TimeSpan PlayerDob = (TimeSpan)(DateTime.Now - convertedDob);
            if (PlayerDob.TotalDays < 7665)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


            



        //If we leave it as a string then we can do our player.dob to cast and parse into DateTime in this method
        //    TimeSpan PlayerDob = (TimeSpan)(DateTime.Now - player.PlayerDob);
        //    if (PlayerDob.TotalDays < 7665)
        //    {

        //        return false;

        //    }
        //    return true; 
        //}



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
                        PlayerId = entity.PlayerId,
                        PlayerFirstName = entity.PlayerFirstName,
                        PlayerLastName = entity.PlayerLastName,
                        PlayerPhone = entity.PlayerPhone,
                        PlayerEmail = entity.PlayerEmail,
                        PlayerAddress = entity.PlayerAddress,
                        PlayerState = entity.PlayerState,
                        PlayerZipCode = entity.PlayerZipCode,
                        PlayerDob = entity.PlayerDob,
                        AccountCreated = entity.AccountCreated,
                        IsActive = entity.IsActive,
                        CurrentBankBalance = entity.CurrentBankBalance,
                        TierStatus = entity.TierStatus,
                        HasAccessToHighLevelGame = entity.HasAccessToHighLevelGame
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
                        PlayerZipCode = entity.PlayerZipCode,
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
                entity.PlayerZipCode = model.PlayerZipCode;
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
                entity.PlayerZipCode = model.PlayerZipCode;
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

                if (entity.IsActive == true)
                {

                     entity.PlayerClosedAccount = true;
                    entity.CurrentBankBalance = 0;
                    return ctx.SaveChanges() > 0;

                }
                else
                {
                  return true;

                }

            }
        }
    }
}