using System;
using System.Collections.Generic;
using System.Linq;
using Casino.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Casino.WebApi.Startup))]

namespace Casino.WebApi
{
    public partial class Startup
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // run startup SEED method below (will not populate if roles already created)
            //CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            CreateSuperAdmin();
            CreateAdmin();
            CreateUser();
            CreatePlayer();

            // test save changes on seed methods to eliminate need to udpate-database after first run
            _db.SaveChanges();
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

        public void CreatePlayer()
        {
            using (_db)
            {
                _db.Games.Add(
                   new Game() { GameId = 2, GameName = "Craps" });
            }
        }
    }
}


