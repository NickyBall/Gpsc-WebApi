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
        public PeriodModel PowerGenPeriod { get; set; }
        public decimal ElectricGen { get; set; }
        public decimal Irradiation { get; set; }
        public PeriodModel IrradiationPeriod { get; set; }
        public decimal AMB_Temp { get; set; }
        public PeriodModel AMB_TempPeriod { get; set; }
        public PeriodModel HourlyPeriod { get; set; }
        public PeriodModel DailyPeriod { get; set; }
        public PeriodModel MonthlyPeriod { get; set; }
        public PeriodModel YearlyPeriod { get; set; }
        public int UnitScale { get; set; }
        public SharedHolderModel SharedHolder { get; set; }
        public decimal SharedHolderPercentage { get; set; }
        public string PlantLocation { get; set; }
        public int PlantTypeId { get; set; }
        public string PlantType { get; set; }
        public int Order { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string LatestTemperature { get; set; }
        public string ForecastTemperature { get; set; }
        public string TimeZone { get; set; }
    }
}