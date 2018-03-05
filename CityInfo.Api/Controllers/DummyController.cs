namespace CityInfo.Api.Controllers
{
    using CityInfo.Api.DbContexts;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/dummy")]
    public class DummyController : Controller
    {
        private CityInfoContext _context;

        public DummyController(CityInfoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}