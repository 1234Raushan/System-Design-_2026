using FluentValidation;

namespace SchoolERP.Application.Features.Classes.Commands.CreateClass;

public sealed class CreateClassValidator
    : AbstractValidator<CreateClassCommand>
{
    public CreateClassValidator()
    {
        RuleFor(x => x.ClassName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ClassCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}