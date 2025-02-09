using System.Text.Json.Serialization;

namespace Rate.API.Models.CoinMarketCap;

public class Status
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("error_code")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("error_message")]
    public object ErrorMessage { get; set; } = default!;

    [JsonPropertyName("elapsed")]
    public int Elapsed { get; set; }

    [JsonPropertyName("credit_count")]
    public int CreditCount { get; set; }

    [JsonPropertyName("notice")]
    public object Notice { get; set; } = default!;

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
}
