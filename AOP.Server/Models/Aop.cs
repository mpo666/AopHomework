namespace AOP.Server.Models
{
    public class Aop
    {
        public string Country { get; set; } = string.Empty;
        public decimal AvgOrder { get; set; }
        public decimal AvgFreight { get; set; }
        public int OrderCount { get; set; }

        public string ExchangeCurrency { get; set; } = "USD";
        //public decimal ExchangeAvgOrder { get; set; } = decimal.Zero;
        //public decimal ExchangeAvgFreight { get; set; } = decimal.Zero;
    }
}
