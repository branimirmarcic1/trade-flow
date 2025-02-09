using System.Text.Json.Serialization;

namespace Rate.API.Models.CoinMarketCap;

public class Quote
{
    [JsonPropertyName("USD")]
    public USD Usd { get; set; }
}
