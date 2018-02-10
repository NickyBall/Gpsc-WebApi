using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class EnergyGenModel
    {
        public decimal EnergyValue { get; set; }
        public decimal Target { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}