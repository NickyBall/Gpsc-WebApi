﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpscWebApi.Models
{
    public class PlantModel
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public CompanyModel PlantInfo { get; set; }
        public CustomerModel Customer { get; set; }
        public LocationModel Location { get; set; }
        public decimal PowerGen { get; set; }
        public decimal ElectricGen { get; set; }
        public SharedHolderModel SharedHolder { get; set; }
        public decimal SharedHolderPercentage { get; set; }
        public string PlantType { get; set; }
    }
}