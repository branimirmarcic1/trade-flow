namespace Rate.API.Models;

public class ExchangeRate
{
    public string Symbol { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime LastUpdated { get; set; }
}
