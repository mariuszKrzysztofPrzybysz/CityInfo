using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.DataStores;
using CityInfo.Api.DataTransferObjects;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        //[HttpGet]
        //public JsonResult GetCities()
        //{
        //    var tmp = new JsonResult(CitiesDataStore.Current.Cities);
        //    tmp.StatusCode = 200;
        //    return tmp;
        //}

        //[HttpGet]
        //public IActionResult GetCities()
        //{
        //    List<CityDto> citiesToReturn = null;
        //    return Ok(citiesToReturn); // Status code: 204 No content
        //}

        [HttpGet]
        public IActionResult GetCities()
            => Ok(CitiesDataStore.Current.Cities);

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}