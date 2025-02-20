using Marten;
using Rate.API.Kafka;
using Rate.API.Models;

namespace Rate.API.Rates.CreateRate;

public record CreateRateCommand(
    int Id,
    string Symbol,
    decimal Price,
    DateTime LastUpdated
) : ICommand<CreateRateResult>, IRequest<CreateRateResult>;

public record CreateRateResult(int Id);

public record CreateRatesCommand(IEnumerable<CreateRateCommand> Rates) : IRequest<CreateRatesResult>;

public record CreateRatesResult(IEnumerable<int> Ids);

internal class CreateRatesCommandHandler
    (IDocumentSession session, KafkaProducerService kafkaProducerService)
    : IRequestHandler<CreateRatesCommand, CreateRatesResult>
{
    public async Task<CreateRatesResult> Handle(CreateRatesCommand command, CancellationToken cancellationToken)
    {
        List<ExchangeRate> rates = command.Rates.Adapt<List<ExchangeRate>>();

        try
        {
            await kafkaProducerService.SendMessageAsync("Rate created");

            session.Store(rates.ToArray());
            await session.SaveChangesAsync(cancellationToken);
            return new CreateRatesResult(rates.Select(r => r.Id));
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
}
