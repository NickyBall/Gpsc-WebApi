using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class SharedHolderModel
    {
        public int SharedHolderId { get; set; }
        public string SharedHolderName { get; set; }
        public decimal GpscShared { get; set; }
    }
}