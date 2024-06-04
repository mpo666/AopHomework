using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AOP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly NorthwindDbService _northwindDbService;

        public CountriesController(ILogger<CountriesController> logger, NorthwindDbService northwindDbService)
        {
            _logger = logger;
            _northwindDbService = northwindDbService;
        }


        /// <summary>
        /// get all countries from northwind db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Models.Country> Countries()
        {
            return _northwindDbService.GetCountries().ToArray();
        }


    }
}
