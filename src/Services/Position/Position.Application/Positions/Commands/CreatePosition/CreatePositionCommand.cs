using FluentValidation;

namespace Position.Application.Positions.Commands.CreatePosition;

public record CreatePositionCommand(
    string InstrumentId,
    decimal Quantity,
    decimal InitialRate,
    decimal CurrentRate,
    int Side
) : ICommand<CreatePositionResult>;

public record CreatePositionResult(Guid Id);

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(x => x.InstrumentId)
            .NotEmpty().WithMessage("InstrumentId is required")
            .MaximumLength(10).WithMessage("InstrumentId cannot be longer than 10 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.InitialRate)
            .GreaterThan(0).WithMessage("InitialRate must be greater than zero");

        RuleFor(x => x.Side)
            .Must(side => side == 1 || side == -1)
            .WithMessage("Side must be 1 (BUY) or -1 (SELL)");
    }
}