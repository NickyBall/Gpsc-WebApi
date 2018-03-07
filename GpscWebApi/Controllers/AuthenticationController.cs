using GpscWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
