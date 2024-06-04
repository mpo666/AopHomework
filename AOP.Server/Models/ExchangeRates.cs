namespace AOP.Server.Models
{
    public class ExchangeRates
    {
        public double amount { get; set; } = 1;
        public string @base { get; set; } = string.Empty;
        public string date { get; set; } = string.Empty;
        public Dictionary<string, decimal> rates { get; set; } = new Dictionary<string, decimal>();
    }
}
