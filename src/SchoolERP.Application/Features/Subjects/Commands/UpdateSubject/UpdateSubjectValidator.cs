using FluentValidation;

namespace SchoolERP.Application.Features.Subjects.Commands.UpdateSubject;

public sealed class UpdateSubjectValidator
    : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.SubjectName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SubjectCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}