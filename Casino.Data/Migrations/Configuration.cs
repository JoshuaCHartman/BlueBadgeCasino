namespace Casino.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Casino.Data.ApplicationDbContext>
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Casino.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            CreateDefaultRolesAndUsers();


            string guidAsStringOfHouse = GetGuidOfSeededUser("house@casino.com");
            string guidAsStringOfUser1 = GetGuidOfSeededUser("user1@abc.com");
            string guidAsStringOfUser2 = GetGuidOfSeededUser("user2@abc.com");
            string guidAsStringOfUser3 = GetGuidOfSeededUser("user3@abc.com");
            string guidAsStringOfUser4 = GetGuidOfSeededUser("user4@abc.com");
            string guidAsStringOfUser5 =("0052320b-3b0a-4b90-93c5-1d3d4d5ac8cb");
            string guidAsStringOfUser6 =("0052320b-3b0a-4b90-93c5-1d3d4d5a1984");
           string guidAsStringOfUser7 = ("0052320b-3b0a-4b90-93c5-1d3d4d5a1960");
            string guidAsStringOfUser8 = ("0052320b-3b0a-4b90-93c5-1d3d4d5a1950");
            string guidAsStringOfUser9 = ("0052320b-3b0a-4b90-93c5-1d3d4d5a1900");

            //Add Games On Startup
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 1, GameName = "Baccarat", TypeOfGame = GameType.Cards, IsHighStakes = false, MinBet = 1, MaxBet = 100 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 2, GameName = "Blackjack", TypeOfGame = GameType.Cards, IsHighStakes = false, MinBet = 1, MaxBet = 100 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 3, GameName = "Craps", TypeOfGame = GameType.Dice, IsHighStakes = false, MinBet = 1, MaxBet = 100 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 4, GameName = "Roulette", TypeOfGame = GameType.Wheel, IsHighStakes = false, MinBet = 1, MaxBet = 100 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 5, GameName = "Keno", TypeOfGame = GameType.Random_Num, IsHighStakes = false, MinBet = 1, MaxBet = 100 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 11, GameName = "Russian Roulette", TypeOfGame = GameType.Wheel, IsHighStakes = true, MinBet = 0, MaxBet = 0 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 6, GameName = "Baccarat", TypeOfGame = GameType.Cards, IsHighStakes = true, MinBet = 1000, MaxBet = 100000 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 7, GameName = "Blackjack", TypeOfGame = GameType.Cards, IsHighStakes = true, MinBet = 1000, MaxBet = 100000 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 8, GameName = "Craps", TypeOfGame = GameType.Dice, IsHighStakes = true, MinBet = 1000, MaxBet = 100000 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 9, GameName = "Roulette", TypeOfGame = GameType.Wheel, IsHighStakes = true, MinBet = 1000, MaxBet = 100000 });
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 10, GameName = "Keno", TypeOfGame = GameType.Random_Num, IsHighStakes = true, MinBet = 1000, MaxBet = 100000 });


            context.Players.AddOrUpdate(x => x.PlayerFirstName,

               
                
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser1),
                PlayerFirstName = "First",
                PlayerLastName = "Player",
                PlayerEmail = "user1@abc.com",
                PlayerDob = "01/01/2000",
                CurrentBankBalance = 1000,
                AccountCreated = DateTimeOffset.Now,
                TierStatus = TierStatus.gold
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser2),
                PlayerFirstName = "Second",
                PlayerLastName = "Player",
                PlayerEmail = "user2@abc.com",
                PlayerDob = "01/01/1990",
                CurrentBankBalance = 20000,
                AccountCreated = DateTimeOffset.Now
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser3),
                PlayerFirstName = "Third",
                PlayerLastName = "Player",
                PlayerEmail = "user3@abc.com",
                PlayerDob = "01/01/1980",
                CurrentBankBalance = 3000,
                AccountCreated = new DateTime(2019, 12, 25)
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser4),
                PlayerFirstName = "Fourth",
                PlayerLastName = "Player",
                PlayerEmail = "user4@abc.com",
                PlayerDob = "01/01/1970",
                CurrentBankBalance = 4000,
                AccountCreated = new DateTime(2020, 10, 10)              // DateTimeOffset.Now
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser5),
                PlayerFirstName = "Lex",
                PlayerLastName = "Luthor",
                PlayerEmail = "lex@luthor.com",
                PlayerDob = "02/01/1960",
                CurrentBankBalance = 6000,
                AccountCreated = new DateTime(2020, 12, 20),
                TierStatus = TierStatus.gold
            },
                new Player()
                {
                    PlayerId = Guid.Parse(guidAsStringOfUser6),
                    PlayerFirstName = "Bruce",
                    PlayerLastName = "Wayne",
                    PlayerEmail = "bruce@wayne.com",
                    PlayerDob = "02/01/1960",
                    CurrentBankBalance = 30000,
                    AccountCreated = new DateTime(2020, 12, 20),
                    TierStatus = TierStatus.gold
                },
                 new Player()
                 {
                     PlayerId = Guid.Parse(guidAsStringOfUser7),
                     PlayerFirstName = "Other",
                     PlayerLastName = "Guy",
                     PlayerEmail = "other@guy.com",
                     PlayerDob = "02/01/1960",
                     CurrentBankBalance = 300,
                     AccountCreated = new DateTime(2020, 12, 20),
                     TierStatus = TierStatus.bronze
                 },
                new Player()
                {
                    PlayerId = Guid.Parse(guidAsStringOfUser8),
                    PlayerFirstName = "Old",
                    PlayerLastName = "Guy",
                    PlayerEmail = "old@guy.com",
                    PlayerDob = "01/01/1960",
                    CurrentBankBalance = 5100,
                    AccountCreated = new DateTime(2020, 12, 20),
                    TierStatus = TierStatus.gold
                },

                new Player()
                {
                    PlayerId = Guid.Parse(guidAsStringOfUser9),
                    PlayerFirstName = "New",
                    PlayerLastName = "Guy",
                    PlayerEmail = "new@guy.com",
                    PlayerDob = "01/01/1950",
                    CurrentBankBalance = 8000,
                    AccountCreated = new DateTime(2020, 12, 20),
                    TierStatus = TierStatus.gold
                },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfHouse),
                PlayerFirstName = "House",
                PlayerLastName = "Account",
                PlayerEmail = "house@casino.com",
                PlayerDob = "01011999",
                AccountCreated = DateTimeOffset.Now
            }

            );

            context.Bets.AddOrUpdate(x => x.BetId,
               
                new Bet()
               {
                   BetId = 1,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 1,
                   BetAmount = 7,
                   PayoutAmount = 7,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 2,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 2,
                   BetAmount = 10,
                   PayoutAmount = -10,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 3,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 3,
                   BetAmount = 15,
                   PayoutAmount = -15,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 4,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = 300,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 5,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 5,
                   BetAmount = 111,
                   PayoutAmount = 333,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 6,
                    PlayerId = Guid.Parse(guidAsStringOfUser1),
                    GameId = 6,
                    BetAmount = 10,
                    PayoutAmount = 20,
                    DateTimeOfBet = DateTimeOffset.Now,
                    PlayerWonGame = true
                },
                 new Bet()
                 {
                     BetId = 7,
                     PlayerId = Guid.Parse(guidAsStringOfUser1),
                     GameId = 7,
                     BetAmount = 150,
                     PayoutAmount = -150,
                     DateTimeOfBet = DateTimeOffset.Now
                 },
                  new Bet()
                  {
                      BetId = 8,
                      PlayerId = Guid.Parse(guidAsStringOfUser1),
                      GameId = 8,
                      BetAmount = 25,
                      PayoutAmount = -25,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 9,
                       PlayerId = Guid.Parse(guidAsStringOfUser1),
                       GameId = 9,
                       BetAmount = 125,
                       PayoutAmount = -125,
                       DateTimeOfBet = DateTimeOffset.Now
                   },
                    new Bet()
                    {
                        BetId = 10,
                        PlayerId = Guid.Parse(guidAsStringOfUser1),
                        GameId = 1,
                        BetAmount = 33,
                        PayoutAmount = 66,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
                    new Bet()
                    {
                        BetId = 11,
                        PlayerId = Guid.Parse(guidAsStringOfUser1),
                        GameId = 2,
                        BetAmount = 7,
                        PayoutAmount = 7,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
               new Bet()
               {
                   BetId = 12,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 3,
                   BetAmount = 777,
                   PayoutAmount = -777,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 13,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = -100,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 14,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 5,
                   BetAmount = 100,
                   PayoutAmount = 125,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 15,
                   PlayerId = Guid.Parse(guidAsStringOfUser1),
                   GameId = 6,
                   BetAmount = 100,
                   PayoutAmount = 133,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 16,
                    PlayerId = Guid.Parse(guidAsStringOfUser1),
                    GameId = 7,
                    BetAmount = 100,
                    PayoutAmount = -1007,
                    DateTimeOfBet = DateTimeOffset.Now
                },
                 new Bet()
                 {
                     BetId = 17,
                     PlayerId = Guid.Parse(guidAsStringOfUser1),
                     GameId = 8,
                     BetAmount = 666,
                     PayoutAmount = 777,
                     DateTimeOfBet = DateTimeOffset.Now,
                     PlayerWonGame = true
                 },
                  new Bet()
                  {
                      BetId = 18,
                      PlayerId = Guid.Parse(guidAsStringOfUser1),
                      GameId = 9,
                      BetAmount = 107,
                      PayoutAmount = -107,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 19,
                       PlayerId = Guid.Parse(guidAsStringOfUser1),
                       GameId = 1,
                       BetAmount = 25,
                       PayoutAmount = 35,
                       DateTimeOfBet = DateTimeOffset.Now,
                       PlayerWonGame = true
                   },
                    new Bet()
                    {
                        BetId = 20,
                        PlayerId = Guid.Parse(guidAsStringOfUser1),
                        GameId = 2,
                        BetAmount = 166,
                        PayoutAmount = 195,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },


                 new Bet()
                 {
                     BetId = 21,
                     PlayerId = Guid.Parse(guidAsStringOfUser2),
                     GameId = 1,
                     BetAmount = 7,
                     PayoutAmount = 7,
                     DateTimeOfBet = DateTimeOffset.Now,
                     PlayerWonGame = true
                 },
               new Bet()
               {
                   BetId = 22,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 2,
                   BetAmount = 10,
                   PayoutAmount = -10,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 23,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 3,
                   BetAmount = 15,
                   PayoutAmount = -15,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 24,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = 300,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 25,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 5,
                   BetAmount = 111,
                   PayoutAmount = 333,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 26,
                    PlayerId = Guid.Parse(guidAsStringOfUser2),
                    GameId = 6,
                    BetAmount = 10,
                    PayoutAmount = 20,
                    DateTimeOfBet = DateTimeOffset.Now,
                    PlayerWonGame = true
                },
                 new Bet()
                 {
                     BetId = 27,
                     PlayerId = Guid.Parse(guidAsStringOfUser2),
                     GameId = 7,
                     BetAmount = 150,
                     PayoutAmount = -150,
                     DateTimeOfBet = DateTimeOffset.Now
                 },
                  new Bet()
                  {
                      BetId = 28,
                      PlayerId = Guid.Parse(guidAsStringOfUser2),
                      GameId = 8,
                      BetAmount = 25,
                      PayoutAmount = -25,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 29,
                       PlayerId = Guid.Parse(guidAsStringOfUser2),
                       GameId = 9,
                       BetAmount = 125,
                       PayoutAmount = -125,
                       DateTimeOfBet = DateTimeOffset.Now
                   },
                    new Bet()
                    {
                        BetId = 30,
                        PlayerId = Guid.Parse(guidAsStringOfUser2),
                        GameId = 1,
                        BetAmount = 33,
                        PayoutAmount = 66,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
                    new Bet()
                    {
                        BetId = 31,
                        PlayerId = Guid.Parse(guidAsStringOfUser2),
                        GameId = 2,
                        BetAmount = 7,
                        PayoutAmount = 7,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
               new Bet()
               {
                   BetId = 32,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 3,
                   BetAmount = 777,
                   PayoutAmount = -777,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 33,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = -100,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 34,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 5,
                   BetAmount = 100,
                   PayoutAmount = 125,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 35,
                   PlayerId = Guid.Parse(guidAsStringOfUser2),
                   GameId = 6,
                   BetAmount = 100,
                   PayoutAmount = 133,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 36,
                    PlayerId = Guid.Parse(guidAsStringOfUser2),
                    GameId = 7,
                    BetAmount = 100,
                    PayoutAmount = -1007,
                    DateTimeOfBet = DateTimeOffset.Now
                },
                 new Bet()
                 {
                     BetId = 37,
                     PlayerId = Guid.Parse(guidAsStringOfUser2),
                     GameId = 8,
                     BetAmount = 666,
                     PayoutAmount = 777,
                     DateTimeOfBet = DateTimeOffset.Now,
                     PlayerWonGame = true
                 },
                  new Bet()
                  {
                      BetId = 38,
                      PlayerId = Guid.Parse(guidAsStringOfUser2),
                      GameId = 9,
                      BetAmount = 107,
                      PayoutAmount = -107,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 39,
                       PlayerId = Guid.Parse(guidAsStringOfUser2),
                       GameId = 1,
                       BetAmount = 25,
                       PayoutAmount = 35,
                       DateTimeOfBet = DateTimeOffset.Now,
                       PlayerWonGame = true
                   },
                    new Bet()
                    {
                        BetId = 40,
                        PlayerId = Guid.Parse(guidAsStringOfUser2),
                        GameId = 2,
                        BetAmount = 166,
                        PayoutAmount = 195,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
                      new Bet()
                      {
                          BetId = 41,
                          PlayerId = Guid.Parse(guidAsStringOfUser3),
                          GameId = 1,
                          BetAmount = 7,
                          PayoutAmount = 7,
                          DateTimeOfBet = DateTimeOffset.Now,
                          PlayerWonGame = true
                      },
               new Bet()
               {
                   BetId = 42,
                   PlayerId = Guid.Parse(guidAsStringOfUser3),
                   GameId = 2,
                   BetAmount = 10,
                   PayoutAmount = -10,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 43,
                   PlayerId = Guid.Parse(guidAsStringOfUser3),
                   GameId = 3,
                   BetAmount = 15,
                   PayoutAmount = -15,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 44,
                   PlayerId = Guid.Parse(guidAsStringOfUser3),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = 300,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 45,
                   PlayerId = Guid.Parse(guidAsStringOfUser3),
                   GameId = 5,
                   BetAmount = 111,
                   PayoutAmount = 333,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 46,
                    PlayerId = Guid.Parse(guidAsStringOfUser3),
                    GameId = 6,
                    BetAmount = 10,
                    PayoutAmount = 20,
                    DateTimeOfBet = DateTimeOffset.Now,
                    PlayerWonGame = true
                },
                 new Bet()
                 {
                     BetId = 47,
                     PlayerId = Guid.Parse(guidAsStringOfUser4),
                     GameId = 7,
                     BetAmount = 150,
                     PayoutAmount = -150,
                     DateTimeOfBet = DateTimeOffset.Now
                 },
                  new Bet()
                  {
                      BetId =48,
                      PlayerId = Guid.Parse(guidAsStringOfUser4),
                      GameId = 8,
                      BetAmount = 25,
                      PayoutAmount = -25,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 49,
                       PlayerId = Guid.Parse(guidAsStringOfUser4),
                       GameId = 9,
                       BetAmount = 125,
                       PayoutAmount = -125,
                       DateTimeOfBet = DateTimeOffset.Now
                   },
                    new Bet()
                    {
                        BetId = 50,
                        PlayerId = Guid.Parse(guidAsStringOfUser4),
                        GameId = 1,
                        BetAmount = 33,
                        PayoutAmount = 66,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
                    new Bet()
                    {
                        BetId = 51,
                        PlayerId = Guid.Parse(guidAsStringOfUser5),
                        GameId = 2,
                        BetAmount = 7,
                        PayoutAmount = 7,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    },
               new Bet()
               {
                   BetId = 52,
                   PlayerId = Guid.Parse(guidAsStringOfUser5),
                   GameId = 3,
                   BetAmount = 777,
                   PayoutAmount = -777,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 53,
                   PlayerId = Guid.Parse(guidAsStringOfUser6),
                   GameId = 4,
                   BetAmount = 100,
                   PayoutAmount = -100,
                   DateTimeOfBet = DateTimeOffset.Now,
               },
               new Bet()
               {
                   BetId = 54,
                   PlayerId = Guid.Parse(guidAsStringOfUser6),
                   GameId = 5,
                   BetAmount = 100,
                   PayoutAmount = 125,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
               new Bet()
               {
                   BetId = 55,
                   PlayerId = Guid.Parse(guidAsStringOfUser7),
                   GameId = 6,
                   BetAmount = 100,
                   PayoutAmount = 133,
                   DateTimeOfBet = DateTimeOffset.Now,
                   PlayerWonGame = true
               },
                new Bet()
                {
                    BetId = 56,
                    PlayerId = Guid.Parse(guidAsStringOfUser7),
                    GameId = 7,
                    BetAmount = 100,
                    PayoutAmount = -1007,
                    DateTimeOfBet = DateTimeOffset.Now
                },
                 new Bet()
                 {
                     BetId = 57,
                     PlayerId = Guid.Parse(guidAsStringOfUser8),
                     GameId = 8,
                     BetAmount = 666,
                     PayoutAmount = 777,
                     DateTimeOfBet = DateTimeOffset.Now,
                     PlayerWonGame = true
                 },
                  new Bet()
                  {
                      BetId = 58,
                      PlayerId = Guid.Parse(guidAsStringOfUser8),
                      GameId = 9,
                      BetAmount = 107,
                      PayoutAmount = -107,
                      DateTimeOfBet = DateTimeOffset.Now
                  },
                   new Bet()
                   {
                       BetId = 59,
                       PlayerId = Guid.Parse(guidAsStringOfUser9),
                       GameId = 1,
                       BetAmount = 25,
                       PayoutAmount = 35,
                       DateTimeOfBet = DateTimeOffset.Now,
                       PlayerWonGame = true
                   },
                    new Bet()
                    {
                        BetId = 60,
                        PlayerId = Guid.Parse(guidAsStringOfUser9),
                        GameId = 2,
                        BetAmount = 166,
                        PayoutAmount = 195,
                        DateTimeOfBet = DateTimeOffset.Now,
                        PlayerWonGame = true
                    }

                ) ;
        }

        private string GetGuidOfSeededUser(string email)
        {
            var ctx = new ApplicationDbContext();
            var entity =
                ctx
                    .Users
                    .Single(e => e.Email == email);
            return entity.Id;
        }



        public void CreateDefaultRolesAndUsers()
        {
            CreateSuperAdmin();
            CreateAdmin();
            CreateUser();
            //CreatePlayer();
        }

        public void CreateSuperAdmin()
        {
            // bring in roleManager and userManager from Identity framework 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            IdentityRole role = new IdentityRole();

            // CREATE 3 Roles 
            // & SEED First SuperAdmin, Admin, and User (will not create and seed if already exists)
            // PLACE DB SEED data in Configuration.cs in Seed()

            // SUPERADMIN

            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // create Role name, add to rolemanager
                role.Name = "SuperAdmin";
                roleManager.Create(role);


                // seed SuperAdmin 

                // create new user set properties
                ApplicationUser user = new ApplicationUser();
                user.UserName = "HouseAccount";
                user.Email = "house@casino.com";
                user.EmailConfirmed = true;


                // pass in new user and pwd
                var Check = userManager.Create(user, "Test1!");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "SuperAdmin");

                }
                else
                {
                    var exception = new Exception("Could not add default SuperAdmin");

                    var enumerator = Check.Errors.GetEnumerator();
                    foreach (var error in Check.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

            }
        }
        // ADMIN
        public void CreateAdmin()
        {
            // bring in roleManager and userManager from Identity framework 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            IdentityRole role = new IdentityRole();


            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);

                // seed Admin 
                ApplicationUser user = new ApplicationUser();
                user.UserName = "FirstAdmin";
                user.Email = "firstAdmin@casino.com";
                user.EmailConfirmed = true;

                // pass in new user and pwd
                var Check = userManager.Create(user, "Test1!");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");

                }
                else
                {
                    var exception = new Exception("Could not add default Admin");

                    var enumerator = Check.Errors.GetEnumerator();
                    foreach (var error in Check.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }


            }
        }
        //USER
        public void CreateUser()
        {


            // bring in roleManager and userManager from Identity framework 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));


            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("User"))
            {
                role.Name = "User";
                roleManager.Create(role);


                // seed User 
                ApplicationUser user1 = new ApplicationUser();
                ApplicationUser user2 = new ApplicationUser();
                ApplicationUser user3 = new ApplicationUser();
                ApplicationUser user4 = new ApplicationUser();

                user1.UserName = "user1";
                user1.Email = "user1@abc.com";
                user1.EmailConfirmed = true;

                user2.UserName = "user2";
                user2.Email = "user2@abc.com";
                user2.EmailConfirmed = true;

                user3.UserName = "user3";
                user3.Email = "user3@abc.com";
                user3.EmailConfirmed = true;

                user4.UserName = "user4";
                user4.Email = "user4@abc.com";
                user4.EmailConfirmed = true;

                // pass in new user and pwd
                var Check1 = userManager.Create(user1, "Test1!");
                var Check2 = userManager.Create(user2, "Test1!");
                var Check3 = userManager.Create(user3, "Test1!");
                var Check4 = userManager.Create(user4, "Test1!");

                if (Check1.Succeeded)
                {
                    userManager.AddToRole(user1.Id, "User");

                }
                else
                {
                    var exception = new Exception("Could not add default seed User");

                    var enumerator = Check1.Errors.GetEnumerator();
                    foreach (var error in Check1.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }
                if (Check2.Succeeded)
                {
                    userManager.AddToRole(user2.Id, "User");

                }
                else
                {
                    var exception = new Exception("Could not add default seed User");

                    var enumerator = Check2.Errors.GetEnumerator();
                    foreach (var error in Check2.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

                if (Check3.Succeeded)
                {
                    userManager.AddToRole(user3.Id, "User");

                }
                else
                {
                    var exception = new Exception("Could not add default seed User");

                    var enumerator = Check3.Errors.GetEnumerator();
                    foreach (var error in Check3.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

                if (Check4.Succeeded)
                {
                    userManager.AddToRole(user4.Id, "User");

                }
                else
                {
                    var exception = new Exception("Could not add default seed User");

                    var enumerator = Check4.Errors.GetEnumerator();
                    foreach (var error in Check4.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

            }
        }

    }

}

