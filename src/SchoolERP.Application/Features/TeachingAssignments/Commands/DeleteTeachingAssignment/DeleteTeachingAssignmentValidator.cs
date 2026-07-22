using FluentValidation;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.DeleteTeachingAssignment;

public sealed class DeleteTeachingAssignmentValidator
    : AbstractValidator<DeleteTeachingAssignmentCommand>
{
    public DeleteTeachingAssignmentValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}