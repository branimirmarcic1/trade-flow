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
        return default!;
    }
}