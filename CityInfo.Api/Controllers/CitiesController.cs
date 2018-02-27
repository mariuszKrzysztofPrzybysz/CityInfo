﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Controllers
{
    public class CitiesController : Controller
    {
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>()
            {
                new {Id=1, Name="New York City"},
                new {Id=2, Name="Antwerp"}
            });
        }
    }
}