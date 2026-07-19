using FluentValidation;

namespace SchoolERP.Application.Features.Sections.Commands.CreateSection;

public sealed class CreateSectionValidator
    : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionValidator()
    {
        RuleFor(x => x.SectionName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ClassId)
            .GreaterThan(0);
    }
}