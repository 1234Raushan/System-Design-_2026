using FluentValidation;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.UpdateEnrollment;

public sealed class UpdateEnrollmentValidator
    : AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.ClassId)
            .GreaterThan(0);

        RuleFor(x => x.SectionId)
            .GreaterThan(0);

        RuleFor(x => x.RollNumber)
            .GreaterThan(0);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}