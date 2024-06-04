using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AOP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AopController : ControllerBase
    {
        private readonly ILogger<AopController> _logger;
        private readonly NorthwindDbService _northwindDbService;

        public AopController(ILogger<AopController> logger, NorthwindDbService northwindDbService)
        {
            _logger = logger;
            _northwindDbService = northwindDbService;
        }


        [HttpGet("{country}/{currency?}")]
        public async Task <IEnumerable<Models.Aop>> Aop(string country, string currency = "USD")
        {

            var aops = _northwindDbService.GetAop(country);
            if ( currency.ToUpper() != "USD" ) {
                var rates = await CurrencyExchange.GetExchangeRates();

                foreach (var aop in aops)
                {
                    aop.ExchangeCurrency = currency.ToUpper();
                    aop.AvgOrder = aop.AvgOrder * rates.rates[currency.ToUpper()];
                    aop.AvgFreight = aop.AvgFreight * rates.rates[currency.ToUpper()];
                    //aop.ExchangeCurrency = currency.ToUpper();
                    //aop.ExchangeAvgOrder = aop.AvgOrder * rates.rates[currency.ToUpper()];
                    //aop.ExchangeAvgFreight = aop.AvgFreight * rates.rates[currency.ToUpper()];
                }
            }

            return aops;
        }
    }
}
