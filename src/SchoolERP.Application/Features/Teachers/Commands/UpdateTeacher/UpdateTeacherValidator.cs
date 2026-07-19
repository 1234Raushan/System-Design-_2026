using FluentValidation;

namespace SchoolERP.Application.Features.Teachers.Commands.UpdateTeacher;

public sealed class UpdateTeacherValidator
    : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}