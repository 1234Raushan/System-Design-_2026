using FluentValidation;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.UpdateAcademicSession;

public sealed class UpdateAcademicSessionValidator
    : AbstractValidator<UpdateAcademicSessionCommand>
{
    public UpdateAcademicSessionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.SessionName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate);

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate);
    }
}