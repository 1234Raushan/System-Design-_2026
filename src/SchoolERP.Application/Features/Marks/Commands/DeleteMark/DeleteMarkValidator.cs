using FluentValidation;

namespace SchoolERP.Application.Features.Marks.Commands.DeleteMark;

public sealed class DeleteMarkValidator
    : AbstractValidator<DeleteMarkCommand>
{
    public DeleteMarkValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Mark Id is required.");
    }
}