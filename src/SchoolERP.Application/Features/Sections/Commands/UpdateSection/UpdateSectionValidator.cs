using FluentValidation;

namespace SchoolERP.Application.Features.Sections.Commands.UpdateSection;

public sealed class UpdateSectionValidator
    : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.SectionName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ClassId)
            .GreaterThan(0);
    }
}