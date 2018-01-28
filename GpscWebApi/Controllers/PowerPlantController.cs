using GpscWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GpscWebApi.Controllers
{
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
        public List<CountryModel> GetAllCountry()
        {
            DbSet<Country> CountryEntity = Db.Countries;
            List<CountryModel> Countries = CountryEntity.Select(c => new CountryModel()
            {
                CountryId = c.Id,
                CountryName = c.Country_Name,
                Location = new LocationModel()
                {
                    Lat = (double)c.Country_Latitude,
                    Lng = (double)c.Country_Longitude
                }
            }).ToList();

            return Countries;
        }

        public List<PlantModel> GetPlantByCountry([FromUri] int CountryId)
        {
            List<Plant> Plants = Db.Countries.FirstOrDefault(c => c.Id == CountryId).Plants.ToList();

            return Plants.Select(p => new PlantModel()
            {
                PlantId = p.ID,
                PlantName = p.Company.Company_Name
            }).ToList();
        }
    }
}
