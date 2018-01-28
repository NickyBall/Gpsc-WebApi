using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GpscWebApi.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        public string Login([FromBody] JObject Body)
        {
            string UserCode = null;
            if (Body["Username"].ToString().Equals("admin") && Body["Password"].ToString().Equals("123456")) UserCode = "UserCode123456";
            return UserCode;
        }
    }
}
