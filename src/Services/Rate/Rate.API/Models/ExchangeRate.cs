namespace Rate.API.Models;

public class ExchangeRate : ExchangeRateDto
{
    public int Id { get; set; }
}

public class ExchangeRateDto
{
    public string Symbol { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime LastUpdated { get; set; }
}
