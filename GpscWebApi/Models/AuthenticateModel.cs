using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class AuthenticateModel
    {
        public string UserCode { get; set; }
        public string AccessToken { get; set; }
    }
}