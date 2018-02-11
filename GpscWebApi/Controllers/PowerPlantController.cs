using GpscWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GpscWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PowerPlantController : ApiController
    {
        pms_devEntities _Db;
        pms_devEntities Db
        {
            get
            {
                if (_Db == null) _Db = new pms_devEntities();
                return _Db;
            }
            set => _Db = value;
        }

        [HttpPost]
        public ResultModel<List<CountryModel>> GetAllCountry([FromBody] JObject Body)
        {
            if (!CheckAuthorize(Body["UserCode"].ToString()))
            {
                return new ResultModel<List<CountryModel>>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = "Unauthorize."
                };
            }

            DbSet<Country> CountryEntity = Db.Countries;
            List<CountryModel> Countries = CountryEntity.Select(c => new CountryModel()
            {
                CountryId = c.Id,
                CountryName = c.Country_Name,
                Location = new LocationModel()
                {
                    Lat = c.Country_Latitude,
                    Lng = c.Country_Longitude
                }
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
            if (!CheckAuthorize(Body["UserCode"].ToString()))
            {
                return new ResultModel<List<PlantModel>>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = "Unauthorize."
                };
            }
            int CountryId = (int)Body["CountryId"];

            List<Plant> Plants = Db.Countries.FirstOrDefault(c => c.Id == CountryId).Plants.ToList();
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
                    PPA = p.Company.PPA
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
                PlantType = p.PlantType.PlantType_Type,
                PowerGen = p.Power_Gen,
                ElectricGen = p.Electricity_Gen,
                SharedHolder = new SharedHolderModel()
                {
                    SharedHolderId = p.SharedHolder.Id,
                    SharedHolderName = p.SharedHolder.SharedHolder_Name,
                    GpscShared = p.SharedHolder.Gpsc_Share
                },
                SharedHolderPercentage = p.SharedHolder_Percentage
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
            if (!CheckAuthorize(Body["UserCode"].ToString()))
            {
                return new ResultModel<PlantModel>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = "Unauthorize."
                };
            }
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
                    PPA = Plant.Company.PPA
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
                PlantType = Plant.PlantType.PlantType_Type,
                PowerGen = Plant.Power_Gen,
                ElectricGen = Plant.Electricity_Gen,
                SharedHolder = new SharedHolderModel()
                {
                    SharedHolderId = Plant.SharedHolder.Id,
                    SharedHolderName = Plant.SharedHolder.SharedHolder_Name,
                    GpscShared = Plant.SharedHolder.Gpsc_Share
                },
                SharedHolderPercentage = Plant.SharedHolder_Percentage
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
            if (!CheckAuthorize(Body["UserCode"].ToString()))
            {
                return new ResultModel<List<EnergyGenModel>>()
                {
                    ResultCode = HttpStatusCode.Unauthorized.GetHashCode(),
                    Message = "Unauthorize."
                };
            }
            int CompanyId = (int)Body["CompanyId"];
            DateTime StartDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            DateTime EndDate = new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
            List<EnergyGenModel> Models = new List<EnergyGenModel>();
            List<Plant> Plants = Db.Plants.Where(p => p.CompanyID == CompanyId && p.CreatedBy > StartDate && p.CreatedBy < EndDate).ToList();

            foreach (Plant Plant in Plants)
            {
                Models.Add(new EnergyGenModel()
                {
                    EnergyValue = Decimal.Parse(Plant.EnergyGen.EnergyGen_Value),
                    Target = -1,
                    TimeStamp = Plant.EnergyGen.EnergyGen_LatestUpdate
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

        private bool CheckAuthorize(string UserCode) => UserCode.Equals("UserCode123456");
    }
}
