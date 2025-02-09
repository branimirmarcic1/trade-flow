using BuildingBlocks.CQRS;
using Rate.API.Models;

namespace Rate.API.Rates.GetRates;

public record GetRatesQuery() : IQuery<GetRatesResult>;
public record GetRatesResult(IEnumerable<ExchangeRate> ExchangeRates);

internal class GetProductsQueryHandler
    : IQueryHandler<GetRatesQuery, GetRatesResult>
{
    public async Task<GetRatesResult> Handle(GetRatesQuery query, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        List<ExchangeRate> dummyExchangeRates =
        [
            new() {  Symbol = "BTC", Price = 50000.0m, LastUpdated = DateTime.UtcNow },
            new() { Symbol = "ETH", Price = 4000.0m, LastUpdated = DateTime.UtcNow },
            new() { Symbol = "XRP", Price = 1.2m, LastUpdated = DateTime.UtcNow }
        ];

        return new GetRatesResult(dummyExchangeRates);
    }
}