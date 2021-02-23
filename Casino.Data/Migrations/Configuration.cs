namespace Casino.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
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
            string guidAsStringOfUser = GetGuidOfSeededUser("abcdef");
            context.Games.AddOrUpdate(x => x.GameId,
               new Game() { GameId = 1, GameName = "BlackJack" });

            context.Players.AddOrUpdate(x => x.PlayerId,
            //new Player()
            //{
            //    PlayerId = Guid.Parse("6c17382b787440438cf7f9da8f5a8873"), PlayerFirstName = "Jon", PlayerLastName = "Dikteruk",
            //    PlayerEmail = "jd@casino.com", PlayerDob = "unknown", AccountCreated = DateTimeOffset.Now
            //},
            //new Player()
            //{
            //    PlayerId = Guid.Parse("1b17382b787440438cf7f9da8f5a8873"), PlayerFirstName = "Fake", PlayerLastName = "One",
            //    PlayerEmail = "f1@casino.com", PlayerDob = "unknown", AccountCreated = DateTimeOffset.Now
            //},
            //new Player()
            //{
            //    PlayerId = Guid.Parse("2b17382b787440438cf7f9da8f5a8873"), PlayerFirstName = "Fake", PlayerLastName = "Two",
            //    PlayerEmail = "f2@casino.com", PlayerDob = "unknown", AccountCreated = DateTimeOffset.Now
            //}, new Player()
            //{
            //    PlayerId = Guid.Parse("3b17382b787440438cf7f9da8f5a8873"), PlayerFirstName = "Fake", PlayerLastName = "Three",
            //    PlayerEmail = "f3@casino.com", PlayerDob = "unknown", AccountCreated = DateTimeOffset.Now
            //},
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfUser), PlayerFirstName = "First", PlayerLastName = "Player", PlayerEmail = "abcdef",
                PlayerDob = "01012000", AccountCreated = DateTimeOffset.Now
            },
            new Player()
            {
                PlayerId = Guid.Parse(guidAsStringOfHouse), PlayerFirstName = "House", PlayerLastName = "Account", PlayerEmail = "house@casino.com",
                PlayerDob = "01011999", AccountCreated = DateTimeOffset.Now
            }

            );

            context.Bets.AddOrUpdate(x => x.BetId,
               //new Bet()
               //{
               //    BetId = 101, PlayerId = Guid.Parse("1b17382b787440438cf7f9da8f5a8873"), GameId = 1, BetAmount = 5, PayoutAmount = 5, DateTimeOfBet = DateTimeOffset.Now,
               //},
               //new Bet()
               //{
               //    BetId = 102, PlayerId = Guid.Parse("1b17382b787440438cf7f9da8f5a8873"), GameId = 1, BetAmount = 5, PayoutAmount = -5, DateTimeOfBet = DateTimeOffset.Now,
               //},
               //new Bet()
               //{
               //    BetId = 103, PlayerId = Guid.Parse("4544850e9f694fdba953116a21ae5c43"), GameId = 1, BetAmount = 15, PayoutAmount = 15, DateTimeOfBet = DateTimeOffset.Now,
               //},
               new Bet()
               {
                   BetId = 1, PlayerId = Guid.Parse(guidAsStringOfUser), GameId = 1, BetAmount = 777, PayoutAmount = 777, DateTimeOfBet = DateTimeOffset.Now,
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
                ApplicationUser user = new ApplicationUser();

                user.UserName = "ThirdSeedUserAccount";
                user.Email = "abcdef";
                user.EmailConfirmed = true;

                // pass in new user and pwd
                var Check = userManager.Create(user, "Test1!");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");

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
