using FluentValidation;

namespace SchoolERP.Application.Features.Timetables.Commands.UpdateTimetable;

public sealed class UpdateTimetableValidator
    : AbstractValidator<UpdateTimetableCommand>
{
    public UpdateTimetableValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.TeachingAssignmentId)
            .GreaterThan(0);

        RuleFor(x => x.DayOfWeek)
            .IsInEnum();

        RuleFor(x => x.PeriodNumber)
            .GreaterThan(0);

        RuleFor(x => x.StartTime)
            .Must(x => x != default)
            .WithMessage("Start Time is required.");

        RuleFor(x => x.EndTime)
            .Must(x => x != default)
            .WithMessage("End Time is required.")
            .Must((model, endTime) => endTime > model.StartTime)
            .WithMessage("End Time must be greater than Start Time.");

        RuleFor(x => x.RoomNumber)
            .MaximumLength(50);

        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}