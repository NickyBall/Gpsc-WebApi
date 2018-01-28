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
        public List<CountryModel> GetAllCountry()
        {

            pms_devEntities DbEntity = new pms_devEntities();
            DbSet<Country> CountryEntity = DbEntity.Countries;
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
            
            //List<Country> CountryList = new List<Country>();
            //{
            //    new Country()
            //    {
            //        Order = 1,
            //        Name = "Thailand",
            //        Location = new Geolocation()
            //        {
            //            Lat = 1,
            //            Lng = 2
            //        },
            //        Categories = new List<Category>()
            //        {
            //            new Category()
            //            {
            //                Name = "cat1",
            //                ImgUrl = "xxx"
            //            },
            //            new Category()
            //            {
            //                Name = "cat2",
            //                ImgUrl = "yyy"
            //            }
            //        }
            //    },
            //    new Country()
            //    {
            //        Order = 2,
            //        Name = "China",
            //        Location = new Geolocation()
            //        {
            //            Lat = 2,
            //            Lng = 3
            //        },
            //        Categories = new List<Category>()
            //        {
            //            new Category()
            //            {
            //                Name = "cat1",
            //                ImgUrl = "xxx"
            //            },
            //            new Category()
            //            {
            //                Name = "cat2",
            //                ImgUrl = "yyy"
            //            }
            //        }
            //    },
            //};

            return Countries;
        }
    }
}
