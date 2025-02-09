using System.Text.Json.Serialization;

namespace Rate.API.Models.CoinMarketCap;

public class Platform
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("token_address")]
    public string TokenAddress { get; set; }
}
