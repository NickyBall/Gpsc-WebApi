using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class PlantModel
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public LocationModel Location { get; set; }
    }
}