using Marten;
using Rate.API.Models;

namespace Rate.API.Rates.GetRates;

public record GetRatesQuery() : IQuery<GetRatesResult>;
public record GetRatesResult(IEnumerable<ExchangeRateDto> ExchangeRates, List<RateVariationAlert> Variations);

internal class GetRatesQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetRatesQuery, GetRatesResult>
{
    private const decimal VARIATION_THRESHOLD_PERCENT = 5;

    public async Task<GetRatesResult> Handle(GetRatesQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<ExchangeRate> rates = GenerateTestExchangeRates();

        List<ExchangeRateDto> exchangeRates = rates.Adapt<List<ExchangeRateDto>>();

        HashSet<string> symbols = new HashSet<string>(rates.Select(r => r.Symbol));
        List<RateVariationAlert> rateVariations = [];

        foreach (string symbol in symbols)
        {
            ExchangeRate? oldestRate = rates
                .Where(r => r.Symbol == symbol)
                .MinBy(r => r.LastUpdated);

            ExchangeRate? latestRate = rates
                .Where(r => r.Symbol == symbol)
                .MaxBy(r => r.LastUpdated);

            if (oldestRate != null && latestRate != null)
            {
                decimal percentageChange = ((latestRate.Price - oldestRate.Price) / oldestRate.Price) * 100;

                if (Math.Abs(percentageChange) > VARIATION_THRESHOLD_PERCENT)
                {
                    rateVariations.Add(new RateVariationAlert(symbol, oldestRate.Price, latestRate.Price, percentageChange));
                }
            }
        }

        return new GetRatesResult(exchangeRates, rateVariations);
    }
    private List<ExchangeRate> GenerateTestExchangeRates()
    {
        List<ExchangeRate> exchangeRates = [];
        Random random = new Random();
        DateTimeOffset oldestRateTime = DateTimeOffset.UtcNow.AddMinutes(2);
        DateTimeOffset latestRateTime = DateTimeOffset.UtcNow;
        List<string> symbols = ["MNT", "ICP", "ETC", "TAO", "KAS"];

        foreach (string symbol in symbols)
        {
            decimal oldPrice = Math.Round((decimal)(random.NextDouble() * (70000 - 500) + 500), 2);
            decimal priceChangeFactor = (decimal)(random.NextDouble() * (1.10 - 0.90) + 0.90);
            decimal newPrice = Math.Round(oldPrice * priceChangeFactor, 2);

            exchangeRates.Add(new ExchangeRate
            {
                Symbol = symbol,
                Price = oldPrice,
                LastUpdated = oldestRateTime
            });

            exchangeRates.Add(new ExchangeRate
            {
                Symbol = symbol,
                Price = newPrice,
                LastUpdated = latestRateTime
            });
        }

        return exchangeRates;
    }
}