using FluentValidation;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.CreateTeachingAssignment;

public sealed class CreateTeachingAssignmentValidator
    : AbstractValidator<CreateTeachingAssignmentCommand>
{
    public CreateTeachingAssignmentValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0);

        RuleFor(x => x.SubjectId)
            .GreaterThan(0);

        RuleFor(x => x.ClassId)
            .GreaterThan(0);

        RuleFor(x => x.SectionId)
            .GreaterThan(0);
    }
}