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
            string guidAsStringOfUser2 = GetGuidOfSeededUser("user1@abc.com");
            string guidAsStringOfUser3 = GetGuidOfSeededUser("user1@abc.com");
            string guidAsStringOfUser4 = GetGuidOfSeededUser("user1@abc.com");


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
            //Russian Roulette need to get players bank balance for min/Max bet
            //context.Games.AddOrUpdate(x => x.GameId,
            //   new Game() {GameId = 11, GameName = "Russian Roulette", TypeOfGame = GameType.Wheel, IsHighStakes = true, MinBet = , MaxBet = 0 });
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


            context.Players.AddOrUpdate(x => x.PlayerId,

            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser1),
                PlayerFirstName = "First",
                PlayerLastName = "Player",
                PlayerEmail = "user1@abc.com",
                PlayerDob = "01/01/2000",
                CurrentBankBalance = 1000,
                AccountCreated = DateTimeOffset.Now
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser1),
                PlayerFirstName = "Second",
                PlayerLastName = "Player",
                PlayerEmail = "user2@abc.com",
                PlayerDob = "01/01/1990",
                CurrentBankBalance = 2000,
                AccountCreated = DateTimeOffset.Now
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser1),
                PlayerFirstName = "Third",
                PlayerLastName = "Player",
                PlayerEmail = "user3@abc.com",
                PlayerDob = "01/01/1980",
                CurrentBankBalance = 3000,
                AccountCreated = new DateTime(2019, 12,25)
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser1),
                PlayerFirstName = "Fourth",
                PlayerLastName = "Player",
                PlayerEmail = "user4@abc.com",
                PlayerDob = "01/01/1970",
                CurrentBankBalance = 4000,
                AccountCreated =  new DateTime (2020,10,10)              // DateTimeOffset.Now
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
                   BetAmount = 777,
                   PayoutAmount = 777,
                   DateTimeOfBet = DateTimeOffset.Now,
               }

                );
        }

        private string GetGuidOfSeededUser(string email)
        {
            var ctx = new ApplicationDbContext();
            var entity =
            ctx.Users


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
            // bring in roleManager and userManager from entity framework 
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
            // bring in roleManager and userManager from entity framework 
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


            // bring in roleManager and userManager from entity framework 
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

                user1.UserName = "user2";
                user1.Email = "user2@abc.com";
                user1.EmailConfirmed = true;

                user1.UserName = "user3";
                user1.Email = "user3@abc.com";
                user1.EmailConfirmed = true;

                user1.UserName = "user4";
                user1.Email = "user4@abc.com";
                user1.EmailConfirmed = true;

                // pass in new user and pwd
                var Check = userManager.Create(user1, "Test");
                userManager.Create(user2, "Test");
                userManager.Create(user3, "Test");
                userManager.Create(user4, "Test");

                if (Check.Succeeded)
                {
                    userManager.AddToRole(user1.Id, "User");
                    userManager.AddToRole(user2.Id, "User");
                    userManager.AddToRole(user3.Id, "User");
                    userManager.AddToRole(user4.Id, "User");

                }
                else
                {
                    var exception = new Exception("Could not add default seed User");

                    var enumerator = Check.Errors.GetEnumerator();
                    foreach (var error in Check.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }


            }
        }



    }
}
