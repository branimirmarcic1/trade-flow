namespace Position.Application.Dtos;

public record PositionDto(
    Guid Id,
    string InstrumentId,
    decimal Quantity,
    decimal InitialRate,
    decimal CurrentRate,
    int Side
);