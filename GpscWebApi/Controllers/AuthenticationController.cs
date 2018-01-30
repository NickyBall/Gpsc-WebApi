using GpscWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        [HttpPost]
        public AuthenticateModel Login([FromBody] JObject Body)
        {
            AuthenticateModel Result = new AuthenticateModel()
            {
                ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                UserCode = ""
            };
            if (Body["Username"].ToString().Equals("admin") && Body["Password"].ToString().Equals("123456"))
            {
                Result.ResultCode = HttpStatusCode.OK.GetHashCode();
                Result.UserCode = "UserCode123456";
            }
            return Result;
        }
    }
}
