using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.DataStores;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet]
        public JsonResult GetCities()
            => new JsonResult(CitiesDataStore.Current.Cities);

        [HttpGet("{id}")]
        public JsonResult GetCity(int id)
            => new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
    }
}