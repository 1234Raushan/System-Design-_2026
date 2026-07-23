using FluentValidation;

namespace SchoolERP.Application.Features.Exams.Commands.DeleteExam;

public sealed class DeleteExamValidator
    : AbstractValidator<DeleteExamCommand>
{
    public DeleteExamValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}