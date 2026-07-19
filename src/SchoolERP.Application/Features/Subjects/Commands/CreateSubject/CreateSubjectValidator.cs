using FluentValidation;

namespace SchoolERP.Application.Features.Subjects.Commands.CreateSubject;

public sealed class CreateSubjectValidator
    : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectValidator()
    {
        RuleFor(x => x.SubjectName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.SubjectCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}