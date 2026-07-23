using FluentValidation;

namespace SchoolERP.Application.Features.Marks.Commands.CreateMark;

public sealed class CreateMarkValidator
    : AbstractValidator<CreateMarkCommand>
{
    public CreateMarkValidator()
    {
        RuleFor(x => x.ExamId)
            .GreaterThan(0)
            .WithMessage("Exam is required.");

        RuleFor(x => x.Students)
            .NotNull()
            .WithMessage("Students are required.")
            .Must(x => x.Any())
            .WithMessage("At least one student is required.");

        RuleForEach(x => x.Students)
            .SetValidator(new CreateMarkItemValidator());


        RuleFor(x => x.Students)
            .Must(HaveUniqueStudents)
            .WithMessage("Duplicate student enrollment found.");
    }


    private static bool HaveUniqueStudents(
        List<CreateMarkItem> students)
    {
        if (students == null || students.Count == 0)
            return true;


        return students
            .Select(x => x.StudentEnrollmentId)
            .Distinct()
            .Count()
            == students.Count;
    }
}

public sealed class CreateMarkItemValidator
    : AbstractValidator<CreateMarkItem>
{
    public CreateMarkItemValidator()
    {
        RuleFor(x => x.StudentEnrollmentId)
            .GreaterThan(0)
            .WithMessage("Student enrollment is required.");


        RuleFor(x => x.ObtainedMarks)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Marks cannot be negative.");


        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}