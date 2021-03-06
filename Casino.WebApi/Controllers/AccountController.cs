using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Casino.WebApi.Models;
using Casino.Data;
using System.Linq;

namespace Casino.WebApi.Controllers
{
   // Add endpoints belovw (inside accountcontroller)

    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // ADD ACCOUNT CONTROLLER ENDPOINTS FROM THIS POINT ON:

        // POST api/Account/Register
        /// <summary>
        ///  Register a new user account with an email and password
        /// </summary>
        
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register_New_Account")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            // authetication check - wont allow authenticated user to create another user

            bool isAuthenticated = User.Identity.IsAuthenticated;

            if (isAuthenticated == true)
            {
                return BadRequest("User authenticated and cannot create another user");
            }

            var _db = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            // Create new role instance
            IdentityRole role = new IdentityRole();

            // Create new 
            IdentityResult result;
            using (_db)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

                role.Name = "User";

                await roleManager.CreateAsync(role);

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                var Check = await UserManager.CreateAsync(user, model.Password);

                if (Check.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user.Id, "User");
                }
                else
                {
                    var exception = new Exception("Could not add user");

                    var enumerator = Check.Errors.GetEnumerator();
                    foreach (var error in Check.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

                return Ok("User Created");

            }

        }

        // ADDED FOR SUPERADMIN TO GET ALL USERS
        // GET api/account/user
        /// <summary>
        /// Return all users - restricted to SuperAdmin
        /// </summary>
        
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin")] // limits to superadmin
                                          
        [Route("Get_AllUsers(SUPERADMIN)")] // display route
        public List<ApplicationUser> GetAllUsers()
        {

            var ctx = new ApplicationDbContext();
            List<ApplicationUser> users = ctx.Users.ToList();

            return users;
        }

        // POST api/Account/Register
        /// <summary>
        /// Create an Admin account - restriced to SuperAdmin, Admin
        /// </summary>
        
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin, Admin")]
        [Route("Create_Admin(SUPERADMIN)")]
        public async Task<IHttpActionResult> CreateAdmin(RegisterBindingModel model)
        {
            var _db = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create new role instance
            IdentityRole role = new IdentityRole();

            // Create new 
            IdentityResult result;
            using (_db)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

                role.Name = "Admin";

                await roleManager.CreateAsync(role);

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                var Check = await UserManager.CreateAsync(user, model.Password);

                if (Check.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user.Id, "Admin");
                }
                else
                {
                    var exception = new Exception("Could not add admin");

                    var enumerator = Check.Errors.GetEnumerator();
                    foreach (var error in Check.Errors)
                    {
                        exception.Data.Add(enumerator.Current, error);
                    }
                    throw exception;

                }

                return Ok("Admin Created");

            }
        }


        // keep : update as needed ?
        // GET api/Account/UserInfo
        /// <summary>
        /// Get user account info
        /// </summary>
        
        /// <returns></returns>
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("Get_UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            return new UserInfoViewModel
            {
                Id = Guid.Parse(User.Identity.GetUserId()), //added category so we could grab this info
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // POST api/Account/Logout
        /// <summary>
        /// Logout of your user account
        /// </summary>
        
        /// <returns></returns>
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/ChangePassword
        
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        // POST api/Account/SetPassword
        /// <summary>
        /// Change your user account password 
        /// </summary>
        
        /// <returns></returns>
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        

        // Admin functions below : 

        // POST api/Account/RemoveLogin

        //[Authorize(Roles = "SuperAdmin, Admin")]
        
        //[Route("RemoveLogin")]
        //public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result;

        //    if (model.LoginProvider == LocalLoginProvider)
        //    {
        //        result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
        //            new UserLoginInfo(model.LoginProvider, model.ProviderKey));
        //    }

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}


        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
