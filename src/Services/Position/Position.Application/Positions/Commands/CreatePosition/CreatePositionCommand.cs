using FluentValidation;
using Position.Application.Dtos;

namespace Position.Application.Positions.Commands.CreatePosition;

public record CreatePositionCommand(PositionDto Position)
    : ICommand<CreatePositionResult>;

public record CreatePositionResult(Guid Id);

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(x => x.Position.InstrumentId)
            .NotEmpty().WithMessage("InstrumentId is required")
            .MaximumLength(10).WithMessage("InstrumentId cannot be longer than 10 characters");

        RuleFor(x => x.Position.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.Position.InitialRate)
            .GreaterThan(0).WithMessage("InitialRate must be greater than zero");

        RuleFor(x => x.Position.Side)
            .Must(side => side == 1 || side == -1)
            .WithMessage("Side must be 1 (BUY) or -1 (SELL)");
    }
}