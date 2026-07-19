using FluentValidation;

namespace SchoolERP.Application.Features.TeacherClasses.Commands.AssignClass;

public sealed class AssignClassValidator
    : AbstractValidator<AssignClassCommand>
{
    public AssignClassValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0);

        RuleFor(x => x.ClassId)
            .GreaterThan(0);
    }
}