using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class ResultModel<o>
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public o Result { get; set; }
    }
}