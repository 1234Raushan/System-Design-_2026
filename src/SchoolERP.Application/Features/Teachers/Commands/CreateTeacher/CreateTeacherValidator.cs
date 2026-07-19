using FluentValidation;

namespace SchoolERP.Application.Features.Teachers.Commands.CreateTeacher;

public sealed class CreateTeacherValidator
    : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.EmployeeCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.JoiningDate)
            .NotEmpty();
    }
}