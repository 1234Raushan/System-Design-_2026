using FluentValidation;

namespace SchoolERP.Application.Features.Exams.Commands.UpdateExam;

public sealed class UpdateExamValidator
    : AbstractValidator<UpdateExamCommand>
{
    public UpdateExamValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.ExamName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ExamType)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ExamDate)
            .NotEmpty();

        RuleFor(x => x.MaximumMarks)
            .GreaterThan(0);

        RuleFor(x => x.PassingMarks)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PassingMarks)
            .LessThanOrEqualTo(x => x.MaximumMarks);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}