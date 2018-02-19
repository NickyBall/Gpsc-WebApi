using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class EnergyGenModel
    {
        public double EnergyValue { get; set; }
        public double Target { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}