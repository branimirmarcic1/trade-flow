using Position.Application.Positions.Commands.CreatePosition;
using Position.Domain.ValueObjects;
using PositionModel = Position.Domain.Models.Position;

namespace Position.Application.Mapping;

public class MapsterSettings
{
    public static void Configure()
    {
        TypeAdapterConfig<CreatePositionCommand, PositionModel>.NewConfig()
            .ConstructUsing(src => PositionModel.Create(
                PositionId.Of(Guid.NewGuid()),
                src.InstrumentId,
                src.Quantity,
                src.InitialRate,
                src.Side
            ));
    }
}