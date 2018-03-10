using GpscWebApi.Identities;
using GpscWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GpscWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticationController : ApiController
    {
        private string CurrentDomainPath => "LDAP://DC=pttgrp,DC=corp";

        [HttpPost]
        public ResultModel<AuthenticateModel> Login([FromBody] JObject Body)
        {
            string Username = Body["Username"].ToString();
            string Password = Body["Password"].ToString();

            try
            {
                DirectoryEntry de = new DirectoryEntry(CurrentDomainPath, Username, Password);
                DirectorySearcher dsearch = new DirectorySearcher(de);
                SearchResult res = null;
                dsearch.PageSize = 100000;
                res = dsearch.FindOne();

                return new ResultModel<AuthenticateModel>()
                {
                    ResultCode = HttpStatusCode.OK.GetHashCode(),
                    Message = "",
                    Result = new AuthenticateModel()
                    {
                        UserCode = "UserCode123456"
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<AuthenticateModel>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = "",
                    Result = new AuthenticateModel()
                    {
                        UserCode = ""
                    }
                };
            }
        }

        [HttpPost]
        public async Task<ResultModel<string>> Register([FromBody] JObject Body)
        {
            ApplicationUserManager UserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser User = new ApplicationUser()
            {
                UserName = Body["Username"].ToString(),
                Email = Body["Email"].ToString(),
                FirstName = Body["Firstname"].ToString(),
                LastName = Body["Lastname"].ToString(),
                JoinDate = DateTime.UtcNow
            };
            IdentityResult Result = await UserManager.CreateAsync(User, Body["Password"].ToString());
            if (Result.Succeeded) return new ResultModel<string>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = $"{User.UserName} has been successfully registered.",
                Result = ""
            };
            return new ResultModel<string>()
            {
                ResultCode = HttpStatusCode.BadRequest.GetHashCode(),
                Message = "Something went wrong",
                Result = ""
            };
        }
    }
}
