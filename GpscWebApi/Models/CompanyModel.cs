using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public decimal Capacity { get; set; }
        public DateTime COD { get; set; }
        public DateTime PPA { get; set; }
        public int IsEnabled { get; set; }
        public List<string> GeneralInfoImages { get; set; }
        public List<string> PlantLayoutImages { get; set; }
        public string Cogen { get; set; }
        public string NavTitle { get; set; }
    }
}