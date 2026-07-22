using FluentValidation;

namespace SchoolERP.Application.Features.Timetables.Commands.DeleteTimetable;

public sealed class DeleteTimetableValidator
    : AbstractValidator<DeleteTimetableCommand>
{
    public DeleteTimetableValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}