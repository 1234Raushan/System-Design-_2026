using FluentValidation;

namespace SchoolERP.Application.Features.Exams.Commands.CreateExam;

public sealed class CreateExamValidator
    : AbstractValidator<CreateExamCommand>
{
    public CreateExamValidator()
    {
        RuleFor(x => x.TeachingAssignmentId)
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
            .LessThanOrEqualTo(x => x.MaximumMarks)
            .WithMessage("Passing marks cannot exceed maximum marks.");

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}