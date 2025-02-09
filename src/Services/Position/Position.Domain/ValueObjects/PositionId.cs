using Position.Domain.Exceptions;

namespace Position.Domain.ValueObjects;

public record PositionId
{
    public Guid Value { get; }
    private PositionId(Guid value)
    {
        Value = value;
    }

    public static PositionId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("PositionId cannot be empty.");
        }

        return new PositionId(value);
    }
}
