using System.Text.Json.Serialization;

namespace Rate.API.Models.CoinMarketCap;

public class Datum
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = default!;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = default!;

    [JsonPropertyName("num_market_pairs")]
    public int NumMarketPairs { get; set; }

    [JsonPropertyName("date_added")]
    public DateTime DateAdded { get; set; }

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; } = default!;

    [JsonPropertyName("max_supply")]
    public float? MaxSupply { get; set; }

    [JsonPropertyName("circulating_supply")]
    public float CirculatingSupply { get; set; }

    [JsonPropertyName("total_supply")]
    public float TotalSupply { get; set; }

    [JsonPropertyName("infinite_supply")]
    public bool InfiniteSupply { get; set; }

    [JsonPropertyName("platform")]
    public Platform Platform { get; set; } = default!;

    [JsonPropertyName("cmc_rank")]
    public int CmcRank { get; set; }

    [JsonPropertyName("self_reported_circulating_supply")]
    public float? SelfReportedCirculatingSupply { get; set; }

    [JsonPropertyName("self_reported_market_cap")]
    public float? SelfReportedMarketCap { get; set; }

    [JsonPropertyName("tvl_ratio")]
    public float? TvlRatio { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }

    [JsonPropertyName("quote")]
    public Quote Quote { get; set; } = default!;
}
