using FluentValidation;

namespace SchoolERP.Application.Features.TeacherSubjects.Commands.AssignSubject;

public sealed class AssignSubjectValidator
    : AbstractValidator<AssignSubjectCommand>
{
    public AssignSubjectValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0);

        RuleFor(x => x.SubjectId)
            .GreaterThan(0);
    }
}