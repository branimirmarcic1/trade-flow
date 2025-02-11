namespace Position.Application.Positions.Commands.ClosePosition;

public record ClosePositionCommand(string InstrumentId) : ICommand<int>;

public class ClosePositionCommandValidator : AbstractValidator<ClosePositionCommand>
{
    public ClosePositionCommandValidator()
    {
        RuleFor(x => x.InstrumentId)
            .NotEmpty().WithMessage("Instrument ID is required.");
    }
}