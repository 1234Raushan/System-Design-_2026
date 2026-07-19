using FluentValidation;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.CreateAcademicSession;

public sealed class CreateAcademicSessionValidator
    : AbstractValidator<CreateAcademicSessionCommand>
{
    public CreateAcademicSessionValidator()
    {
        RuleFor(x => x.SessionName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("Start date must be before end date.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date.");
    }
}