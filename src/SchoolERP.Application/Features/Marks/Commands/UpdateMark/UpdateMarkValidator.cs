using FluentValidation;

namespace SchoolERP.Application.Features.Marks.Commands.UpdateMark;

public sealed class UpdateMarkValidator
    : AbstractValidator<UpdateMarkCommand>
{
    public UpdateMarkValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Mark Id is required.");


        RuleFor(x => x.ObtainedMarks)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Marks cannot be negative.");


        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}