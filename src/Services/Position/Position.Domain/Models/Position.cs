using Position.Domain.Abstractions;
using Position.Domain.Exceptions;
using Position.Domain.ValueObjects;

namespace Position.Domain.Models;

public class Position : Entity<PositionId>
{
    public string InstrumentId { get; private set; } = default!;
    public decimal Quantity { get; private set; }
    public decimal InitialRate { get; private set; }
    public decimal CurrentRate { get; private set; }
    public int Side { get; private set; } // 1 = BUY, -1 = SELL

    private Position() { }

    public static Position Create(PositionId id, string instrumentId, decimal quantity, decimal initialRate, int side)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(instrumentId);

        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be greater than zero.");
        }

        if (initialRate <= 0)
        {
            throw new DomainException("Initial rate must be greater than zero.");
        }

        if (side != 1 && side != -1)
        {
            throw new DomainException("Side must be either 1 (BUY) or -1 (SELL).");
        }

        return new Position
        {
            Id = id,
            InstrumentId = instrumentId,
            Quantity = quantity,
            InitialRate = initialRate,
            CurrentRate = initialRate,
            Side = side
        };
    }

    public void UpdateCurrentRate(decimal newRate)
    {
        if (newRate <= 0)
        {
            throw new DomainException("New rate must be greater than zero.");
        }

        CurrentRate = newRate;
    }

    public decimal CalculateProfitLoss()
    {
        return Quantity * (CurrentRate - InitialRate) * Side;
    }
}
