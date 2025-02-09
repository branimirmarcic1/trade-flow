using PositionModel = Position.Domain.Models.Position;
namespace Position.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<PositionModel> Positions =>
        [
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "BTC/USD", 3m, 58871.01215m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "ETH/USD", 10m, 2682.019189m, -1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "SOL/USD", 20m, 138.5050875m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "BNB/USD", 5m, 512.9499832m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "USDT/USD", 10000m, 1.000134593m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "ADA/USD", 5000m, 0.335245269m, -1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "SHIB/USD", 100000m, 1.36410407145e-05m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "DOGE/USD", 43000m, 0.105241227m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "XRP/USD", 27000m, 0.565457483m, -1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "AVAX/USD", 50m, 21.02913658m, -1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "LTC/USD", 10m, 61.03340933m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "CRO/USD", 80000m, 0.087408805m, 1),
            PositionModel.Create(PositionId.Of(Guid.NewGuid()), "XLM/USD", 63000m, 0.09764245m, -1)
        ];
}
