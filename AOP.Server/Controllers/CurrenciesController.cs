using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AOP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ILogger<CurrenciesController> _logger;
        private readonly NorthwindDbService _northwindDbService;

        public CurrenciesController(ILogger<CurrenciesController> logger, NorthwindDbService northwindDbService)
        {
            _logger = logger;
            _northwindDbService = northwindDbService;
        }


        [HttpGet]
        public IEnumerable<Models.Currency> Countries()
        {
            // fix list from requested currencies
            // could be extend with remote api request from class CurrencyExchange

            List<Models.Currency> currencies = new List<Models.Currency>()
            {
                new Models.Currency() { Name = "USD" },
                new Models.Currency() { Name = "EUR" },
                new Models.Currency() { Name = "GBP" }
            };

            return currencies.ToArray();
        }
    }
}
