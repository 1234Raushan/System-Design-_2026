using FluentValidation;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.PromoteStudent;

public sealed class PromoteStudentValidator
    : AbstractValidator<PromoteStudentCommand>
{
    public PromoteStudentValidator()
    {
        RuleFor(x => x.StudentEnrollmentId)
            .GreaterThan(0);

        RuleFor(x => x.NewAcademicSessionId)
            .GreaterThan(0);

        RuleFor(x => x.NewClassId)
            .GreaterThan(0);

        RuleFor(x => x.NewSectionId)
            .GreaterThan(0);

        RuleFor(x => x.NewRollNumber)
            .GreaterThan(0);
    }
}