using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class AuthenticateModel
    {
        public int ResultCode { get; set; }
        public string UserCode { get; set; }
    }
}