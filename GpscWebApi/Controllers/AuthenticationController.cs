using GpscWebApi.Identities;
using GpscWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GpscWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        private string CurrentDomainPath => "LDAP://DC=pttgrp,DC=corp";

        [HttpPost]
        public async Task<ResultModel<AuthenticateModel>> Login([FromBody] JObject Body)
        {
            string Username = Body["Username"].ToString();
            string Password = Body["Password"].ToString();

            ApplicationUserManager UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser User = await UserManager.FindByNameAsync(Username);

            // Check Lockout
            if (User != null && UserManager.SupportsUserLockout && UserManager.IsLockedOut(User.Id)) return new ResultModel<AuthenticateModel>()
            {
                ResultCode = HttpStatusCode.Forbidden.GetHashCode(),
                Message = $"{User.UserName} has been suspended.",
                Result = null
            };

            // LDAP Authentication
            try
            {
                DirectoryEntry de = new DirectoryEntry(CurrentDomainPath, Username, Password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult res = null;
                dsearch.PageSize = 100000;
                res = dsearch.FindOne();
            }
            catch
            {
                if (User != null && UserManager.SupportsUserLockout) await UserManager.AccessFailedAsync(User.Id);
                return new ResultModel<AuthenticateModel>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = $"No User",
                    Result = new AuthenticateModel()
                    {
                        AccessToken = ""
                    }
                };
            }

            // Register if null
            if (User == null)
            {
                User = new ApplicationUser()
                {
                    UserName = Username,
                    JoinDate = DateTime.UtcNow
                };
                IdentityResult Result = await UserManager.CreateAsync(User, Password);
            }
            else
            {
                //string ResetToken = await UserManager.GeneratePasswordResetTokenAsync(User.Id);
                //var x = await UserManager.ResetPasswordAsync(User.Id, ResetToken, Password);
                User.PasswordHash = UserManager.PasswordHasher.HashPassword(Password);
                var x = await UserManager.UpdateAsync(User);
            }
            
            //// Check Lockout Enable?
            //if (UserManager.SupportsUserLockout)
            //{
            //    // Check Password Correct?
            //    if (await UserManager.CheckPasswordAsync(User, Password)) await UserManager.ResetAccessFailedCountAsync(User.Id);
            //    else
            //    {
            //        await UserManager.AccessFailedAsync(User.Id);
            //        return new ResultModel<AuthenticateModel>()
            //        {
            //            ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //            Message = $"Username or Password is incorrect.",
            //            Result = new AuthenticateModel()
            //            {
            //                UserCode = "",
            //                AccessToken = ""
            //            }
            //        };
            //    }
            //}

            // Get Token
            TokenResponseModel TokenRes = new TokenResponseModel();

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>()
                                {
                                    {"grant_type", "password"},
                                    {"username", Username },
                                    {"password", Password }
                                };
                var BodyParam = new FormUrlEncodedContent(values);
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                // Post for Token
                try
                {
                    var response = await client.PostAsync(ConfigurationManager.AppSettings["TokenEndpoint"], BodyParam);
                    if (response.IsSuccessStatusCode)
                    {
                        string outputJson = await response.Content.ReadAsStringAsync();
                        TokenRes = JsonConvert.DeserializeObject<TokenResponseModel>(outputJson);
                    }
                }
                catch
                {
                    return new ResultModel<AuthenticateModel>()
                    {
                        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                        Message = $"Authorization Fail.",
                        Result = new AuthenticateModel()
                        {
                            AccessToken = ""
                        }
                    };
                }
            }

            return new ResultModel<AuthenticateModel>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = new AuthenticateModel()
                {
                    AccessToken = TokenRes.access_token
                }
            };

        }
    }
}
