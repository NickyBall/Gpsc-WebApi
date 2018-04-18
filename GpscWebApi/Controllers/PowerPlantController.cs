using GpscWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GpscWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    [Authorize]
    public class PowerPlantController : ApiController
    {
        GpscEntities _Db;
        GpscEntities Db
        {
            get
            {
                if (_Db == null) _Db = new GpscEntities();
                return _Db;
            }
            set => _Db = value;
        }

        [HttpPost]
        
        public ResultModel<List<CountryModel>> GetAllCountry([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<CountryModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}

            DbSet<Country> CountryEntity = Db.Countries;
            List<CountryModel> Countries = CountryEntity.Select(c => new CountryModel()
            {
                CountryId = c.Id,
                CountryName = c.Country_Name,
                Location = new LocationModel()
                {
                    Lat = c.Country_Latitude,
                    Lng = c.Country_Longitude
                },
                PlantCount = c.Plants.Count
            }).ToList();

            return new ResultModel<List<CountryModel>>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = Countries
            };
        }

        [HttpPost]
        public ResultModel<List<PlantModel>> GetPlantByCountry([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<PlantModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int CountryId = (int)Body["CountryId"];

            List<Plant> Plants = Db.Countries.FirstOrDefault(c => c.Id == CountryId).Plants.OrderBy(p => p.Order).ToList();
            List<PlantModel> PlantModels = Plants.Select(p => new PlantModel()
            {
                PlantId = p.ID,
                PlantName = p.Company.Company_Name,
                PlantInfo = new CompanyModel()
                {
                    CompanyId = p.Company.ID,
                    CompanyName = p.Company.Company_Name,
                    CompanyLogo = p.Company.Company_Logo_Path,
                    Capacity = p.Company.Capacity,
                    COD = p.Company.COD,
                    PPA = p.Company.PPA,
                    IsEnabled = p.IsEnabled,
                    GeneralInfoImages = p.Company.GeneralInfoImages.Select(c => c.ImageUrl).ToList(),
                    PlantLayoutImages = p.Company.PlantLayoutImages.Select(c => c.ImageUrl).ToList(),
                    Cogen = p.Cogen
                },
                Customer = new CustomerModel()
                {
                    CustomerId = p.Customer.Id,
                    CustomerName = p.Customer.Customer_Name
                },
                Location = new LocationModel()
                {
                    Lat = p.Location_Latitude,
                    Lng = p.Location_Longitude
                },
                PlantTypeId = p.PlantTypeID,
                PlantType = p.PlantType.PlantType_Type,
                PlantLocation = p.Location_Name,
                PowerGen = p.Power_Gen >= 0 ? p.Power_Gen : 0,
                PowerGenPeriod = new PeriodModel()
                {
                    Min = p.Power_Gen_Min,
                    Max = p.Power_Gen_Max,
                    Scale = p.Power_Gen_Scale
                },
                ElectricGen = p.Electricity_Gen,
                Irradiation = p.Irradiation,
                IrradiationPeriod = new PeriodModel()
                {
                    Min = p.Irradiation_Min,
                    Max = p.Irradiation_Max,
                    Scale = p.Irradiation_Scale
                },
                AMB_Temp = p.AMB_Temp,
                AMB_TempPeriod = new PeriodModel()
                {
                    Min = p.AMB_Min,
                    Max = p.AMB_Max,
                    Scale = p.AMB_Scale
                },
                HourlyPeriod = new PeriodModel()
                {
                    Min = p.Hourly_Min,
                    Max = p.Hourly_Max,
                    Scale = p.Hourly_Scale
                },
                DailyPeriod = new PeriodModel()
                {
                    Min = p.Daily_Min,
                    Max = p.Daily_Max,
                    Scale = p.Daily_Scale
                },
                MonthlyPeriod = new PeriodModel()
                {
                    Min = p.Monthly_Min,
                    Max = p.Monthly_Max,
                    Scale = p.Monthly_Scale
                },
                YearlyPeriod = new PeriodModel()
                {
                    Min = p.Yearly_Min,
                    Max = p.Yearly_Max,
                    Scale = p.Yearly_Scale
                },
                UnitScale = p.UnitScale.HasValue ? p.UnitScale.Value : 1,
                UpdatedAt = p.UpdatedAt,
                SharedHolder = new SharedHolderModel()
                {
                    SharedHolderId = p.SharedHolder.Id,
                    SharedHolderName = p.SharedHolder.SharedHolder_Name,
                    GpscShared = p.SharedHolder.Gpsc_Share
                },
                SharedHolderPercentage = p.SharedHolder_Percentage,
                LatestTemperature = p.LatestTemperature,
                ForecastTemperature = p.ForecastTemperature,
                Order = p.Order,
                TimeZone = p.TimeZone
            }).ToList();

            return new ResultModel<List<PlantModel>>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = PlantModels
            };
        }

        [HttpPost]
        public ResultModel<PlantModel> GetPlantInfo([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<PlantModel>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int PlantId = (int)Body["PlantId"];

            Plant Plant = Db.Plants.FirstOrDefault(p => p.ID == PlantId);
            PlantModel PlantInfo = new PlantModel()
            {
                PlantId = Plant.ID,
                PlantName = Plant.Company.Company_Name,
                PlantInfo = new CompanyModel()
                {
                    CompanyId = Plant.Company.ID,
                    CompanyName = Plant.Company.Company_Name,
                    CompanyLogo = Plant.Company.Company_Logo_Path,
                    Capacity = Plant.Company.Capacity,
                    COD = Plant.Company.COD,
                    PPA = Plant.Company.PPA,
                    GeneralInfoImages = Plant.Company.GeneralInfoImages.Select(c => c.ImageUrl).ToList(),
                    PlantLayoutImages = Plant.Company.PlantLayoutImages.Select(c => c.ImageUrl).ToList(),
                    IsEnabled = Plant.IsEnabled,
                    Cogen = Plant.Cogen
                },
                Customer = new CustomerModel()
                {
                    CustomerId = Plant.Customer.Id,
                    CustomerName = Plant.Customer.Customer_Name
                },
                Location = new LocationModel()
                {
                    Lat = Plant.Location_Latitude,
                    Lng = Plant.Location_Longitude
                },
                PlantTypeId = Plant.PlantTypeID,
                PlantType = Plant.PlantType.PlantType_Type,
                PlantLocation = Plant.Location_Name,
                PowerGen = Plant.Power_Gen >= 0 ? Plant.Power_Gen : 0,
                PowerGenPeriod = new PeriodModel()
                {
                    Min = Plant.Power_Gen_Min,
                    Max = Plant.Power_Gen_Max,
                    Scale = Plant.Power_Gen_Scale
                },
                ElectricGen = Plant.Electricity_Gen,
                Irradiation = Plant.Irradiation,
                IrradiationPeriod = new PeriodModel()
                {
                    Min = Plant.Irradiation_Min,
                    Max = Plant.Irradiation_Max,
                    Scale = Plant.Irradiation_Scale
                },
                AMB_Temp = Plant.AMB_Temp,
                AMB_TempPeriod = new PeriodModel()
                {
                    Min = Plant.AMB_Min,
                    Max = Plant.AMB_Max,
                    Scale = Plant.AMB_Scale
                },
                HourlyPeriod = new PeriodModel()
                {
                    Min = Plant.Hourly_Min,
                    Max = Plant.Hourly_Max,
                    Scale = Plant.Hourly_Scale
                },
                DailyPeriod = new PeriodModel()
                {
                    Min = Plant.Daily_Min,
                    Max = Plant.Daily_Max,
                    Scale = Plant.Daily_Scale
                },
                MonthlyPeriod = new PeriodModel()
                {
                    Min = Plant.Monthly_Min,
                    Max = Plant.Monthly_Max,
                    Scale = Plant.Monthly_Scale
                },
                YearlyPeriod = new PeriodModel()
                {
                    Min = Plant.Yearly_Min,
                    Max = Plant.Yearly_Max,
                    Scale = Plant.Yearly_Scale
                },
                UnitScale = Plant.UnitScale.HasValue ? Plant.UnitScale.Value : 1,
                UpdatedAt = Plant.UpdatedAt,
                SharedHolder = new SharedHolderModel()
                {
                    SharedHolderId = Plant.SharedHolder.Id,
                    SharedHolderName = Plant.SharedHolder.SharedHolder_Name,
                    GpscShared = Plant.SharedHolder.Gpsc_Share
                },
                SharedHolderPercentage = Plant.SharedHolder_Percentage,
                LatestTemperature = Plant.LatestTemperature,
                ForecastTemperature = Plant.ForecastTemperature,
                TimeZone = Plant.TimeZone
            };
            return new ResultModel<PlantModel>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = PlantInfo
            };
        }

        [HttpPost]
        public ResultModel<List<EnergyGenModel>> GetHourlyEnergyGen([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<EnergyGenModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int CompanyId = (int)Body["CompanyId"];
            DateTime StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            DateTime EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            List<EnergyGenModel> Models = new List<EnergyGenModel>();

            var StartHour = Int16.Parse(ConfigurationManager.AppSettings["PowerPlant_Hourly_Start"]);
            var EndHour = Int16.Parse(ConfigurationManager.AppSettings["PowerPlant_Hourly_End"]);

            for (int i = StartHour; i <= EndHour; i++)
            {
                DateTime CurrentHour = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, i, 0, 0);

                var hourly = Db.PlantEnergyGenHourlyViews.Where(a =>
                        a.Time_Stamp.Year == CurrentHour.Year &&
                        a.Time_Stamp.Month == CurrentHour.Month &&
                        a.Time_Stamp.Day == CurrentHour.Day &&
                        a.Time_Stamp.Hour == CurrentHour.Hour &&
                        a.PlantId.Equals(CompanyId)
                );
                //.OrderBy(a => a.Time_Stamp).Select(a => new
                //{
                //    Index = a.Row,
                //    EnergyValue = a.AverageEnergyGenValue,
                //    TimeStamp = a.Time_Stamp
                //});
                Models.Add(new EnergyGenModel()
                {
                    EnergyValue = hourly.Count() > 0 ? (double)hourly.FirstOrDefault().AverageEnergyGenValue : 0,
                    Target = -1,
                    TimeStamp = CurrentHour
                });
            }

            

            ResultModel<List<EnergyGenModel>> Result = new ResultModel<List<EnergyGenModel>>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = Models
            };

            return Result;
        }

        [HttpPost]
        public ResultModel<List<EnergyGenModel>> GetDailyEnergyGen([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<EnergyGenModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int CompanyId = (int)Body["CompanyId"];
            DateTime StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);
            List<EnergyGenModel> Models = new List<EnergyGenModel>();

            for (int i = StartDate.Day; i <= EndDate.Day; i++)
            {
                DateTime CurrentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, i);
                var hourly = Db.PlantEnergyGenDailyViews.Where(a => 
                    a.Time_Stamp.Year == CurrentDate.Year && 
                    a.Time_Stamp.Month == CurrentDate.Month &&
                    a.Time_Stamp.Day == CurrentDate.Day &&
                    a.PlantId.Equals(CompanyId)).OrderBy(a => a.Time_Stamp).Select(a => new
                        {
                            Index = a.Row,
                            EnergyValue = a.AverageEnergyGenValue,
                            TimeStamp = a.Time_Stamp
                        });
                Models.Add(new EnergyGenModel()
                {
                    EnergyValue = hourly.Count() > 0 ? (double)hourly.FirstOrDefault().EnergyValue : 0,
                    Target = -1,
                    TimeStamp = CurrentDate
                });
            }

            

            ResultModel<List<EnergyGenModel>> Result = new ResultModel<List<EnergyGenModel>>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = Models
            };

            return Result;
        }

        [HttpPost]
        public ResultModel<List<EnergyGenModel>> GetMonthlyEnergyGen([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<EnergyGenModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int CompanyId = (int)Body["CompanyId"];
            
            List<EnergyGenModel> Models = new List<EnergyGenModel>();

            for (int i = 1; i <= 12; i++)
            {
                DateTime RefDate = new DateTime(DateTime.Today.Year, i, 1);
                var monthly = Db.PlantEnergyGenMonthlyViews.Where(a => a.Time_Stamp.Year == RefDate.Year && a.Time_Stamp.Month == RefDate.Month && a.PlantId.Equals(CompanyId));
                

                string YearMonth = $"{RefDate.Year}-{i.ToString().PadLeft(2, '0')}";
                var Target = Db.EnergyGenTargets.Where(t => t.YearMonth.Equals(YearMonth) && t.PlantId.Equals(CompanyId));
                Models.Add(new EnergyGenModel()
                {
                    EnergyValue = (double)(monthly.Count() > 0 ? monthly.FirstOrDefault().AverageEnergyGenValue : 0),
                    Target = (double)(Target.Count() > 0 ? Target.FirstOrDefault().TargetValue : 0),
                    TimeStamp = RefDate
                });
            }

            ResultModel<List<EnergyGenModel>> Result = new ResultModel<List<EnergyGenModel>>()
            {
                ResultCode = HttpStatusCode.OK.GetHashCode(),
                Message = "",
                Result = Models
            };

            return Result;
        }

        [HttpPost]
        public ResultModel<List<EnergyGenModel>> GetYearlyEnergyGen([FromBody] JObject Body)
        {
            //if (!CheckAuthorize(Body["UserCode"].ToString()))
            //{
            //    return new ResultModel<List<EnergyGenModel>>()
            //    {
            //        ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
            //        Message = "Unauthorize."
            //    };
            //}
            int CompanyId = (int)Body["CompanyId"];
            //DateTime StartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            //DateTime EndDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            List<EnergyGenModel> Models = new List<EnergyGenModel>();
            try
            {
                DateTime Nowadays = DateTime.Now;
                var YearMin = Int16.Parse(ConfigurationManager.AppSettings["PowerPlant_YearMin"]);
                var YearMax = Int16.Parse(ConfigurationManager.AppSettings["PowerPlant_YearMax"]);

                for (int i = (Nowadays.Year - YearMin); i <= (Nowadays.Year + YearMax); i++)
                {
                    var Target = Db.PlantEnergyGenYearTargets.Where(t => t.PlantId.Equals(CompanyId) && t.YearTarget == i.ToString());
                    var yearly = Db.PlantEnergyGenYearlyViews.Where(p => p.PlantId.Equals(CompanyId) && p.Time_Stamp.Year == i);
                    Models.Add(new EnergyGenModel()
                    {
                        EnergyValue = (double)(yearly.Count() > 0 ? yearly.FirstOrDefault().AverageEnergyGenValue : 0),
                        Target = (double)(Target.Count() > 0 ? Target.FirstOrDefault().Total : 0),
                        TimeStamp = new DateTime(i, 1, 1)
                    });
                }

                return new ResultModel<List<EnergyGenModel>>()
                {
                    ResultCode = HttpStatusCode.OK.GetHashCode(),
                    Message = "",
                    Result = Models
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<EnergyGenModel>>()
                {
                    ResultCode = HttpStatusCode.OK.GetHashCode(),
                    Message = ex.Message,
                    Result = null
                };
            }

            
        }

        [HttpGet]
        public List<string> FlushVersion()
        {

            var path = Environment.GetEnvironmentVariable("PATH");
            List<string> Versions = new List<string>();
            // get all
            var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
            while (enumerator.MoveNext())
            {
                Versions.Add($"{enumerator.Key,5}:{enumerator.Value,100}");
            }
            return Versions;
        }

        //private bool CheckAuthorize(string UserCode) => UserCode.Equals("UserCode123456");
    }

}
