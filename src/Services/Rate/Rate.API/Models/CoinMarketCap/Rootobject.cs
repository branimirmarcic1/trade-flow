using System.Text.Json.Serialization;

namespace Rate.API.Models.CoinMarketCap;

public class Rootobject
{
    [JsonPropertyName("status")]
    public Status Status { get; set; } = default!;

    [JsonPropertyName("data")]
    public Datum[] Data { get; set; } = default!;
}
