using Marten;
using Rate.API.Models;

namespace Rate.API.Rates.GetRates;

public record GetRatesQuery() : IQuery<GetRatesResult>;
public record GetRatesResult(IEnumerable<ExchangeRateDto> ExchangeRates, List<RateVariationAlert> Variations);

internal class GetRatesQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetRatesQuery, GetRatesResult>
{
    public async Task<GetRatesResult> Handle(GetRatesQuery query, CancellationToken cancellationToken)
    {
        //IReadOnlyList<ExchangeRate> rates = await session.Query<ExchangeRate>()
        //    .Where(r => r.LastUpdated >= DateTimeOffset.UtcNow.AddHours(-24))
        //    .ToListAsync(cancellationToken);
        IReadOnlyList<ExchangeRate> rates = GenerateTestExchangeRates();

        List<ExchangeRateDto> exchangeRates = rates.Adapt<List<ExchangeRateDto>>();

        IEnumerable<string> symbols = rates.Select(r => r.Symbol).Distinct();
        List<RateVariationAlert> rateVariations = [];

        foreach (string? symbol in symbols)
        {
            ExchangeRate? oldestRate = rates
                .Where(r => r.Symbol == symbol)
                .OrderBy(r => r.LastUpdated)
                .FirstOrDefault();

            ExchangeRate? latestRate = rates
                .Where(r => r.Symbol == symbol)
                .OrderByDescending(r => r.LastUpdated)
                .FirstOrDefault();

            if (oldestRate != null && latestRate != null)
            {
                decimal percentageChange = ((latestRate.Price - oldestRate.Price) / oldestRate.Price) * 100;

                if (Math.Abs(percentageChange) > 5)
                {
                    rateVariations.Add(new RateVariationAlert(symbol, oldestRate.Price, latestRate.Price, percentageChange));
                }
            }
        }

        return new GetRatesResult(exchangeRates, rateVariations);
    }
    private List<ExchangeRate> GenerateTestExchangeRates()
    {
        List<string> symbols = ["MNT", "ICP", "ETC", "TAO", "KAS"];
        List<ExchangeRate> exchangeRates = [];
        Random random = new Random();
        DateTimeOffset oldestRateTime = DateTimeOffset.UtcNow.AddMinutes(-2);
        DateTimeOffset latestRateTime = DateTimeOffset.UtcNow;

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