using FluentValidation;

namespace SchoolERP.Application.Features.Attendance.Commands.CreateAttendance;

public sealed class CreateAttendanceValidator
    : AbstractValidator<CreateAttendanceCommand>
{
    public CreateAttendanceValidator()
    {
        RuleFor(x => x.AcademicSessionId)
            .GreaterThan(0);


        RuleFor(x => x.ClassId)
            .GreaterThan(0);


        RuleFor(x => x.SectionId)
            .GreaterThan(0);


        RuleFor(x => x.Students)
            .NotEmpty()
            .WithMessage("Students required.");
    }
}