using FluentValidation;

namespace SchoolERP.Application.Features.Classes.Commands.UpdateClass;

public sealed class UpdateClassValidator
    : AbstractValidator<UpdateClassCommand>
{
    public UpdateClassValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

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