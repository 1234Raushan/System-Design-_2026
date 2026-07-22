using FluentValidation;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.UpdateTeachingAssignment;

public sealed class UpdateTeachingAssignmentValidator
    : AbstractValidator<UpdateTeachingAssignmentCommand>
{
    public UpdateTeachingAssignmentValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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