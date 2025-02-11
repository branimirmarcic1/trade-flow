namespace Position.Application.Positions.Commands.UpdatePosition;

public record UpdatePositionCommand(
    string InstrumentId,
    decimal NewRate
) : ICommand<bool>;

public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    public UpdatePositionCommandValidator()
    {
        RuleFor(x => x.InstrumentId)
            .NotEmpty().WithMessage("InstrumentId is required")
            .MaximumLength(10).WithMessage("InstrumentId cannot be longer than 10 characters");

        RuleFor(x => x.NewRate)
            .GreaterThan(0).WithMessage("NewRate must be greater than zero");
    }
}