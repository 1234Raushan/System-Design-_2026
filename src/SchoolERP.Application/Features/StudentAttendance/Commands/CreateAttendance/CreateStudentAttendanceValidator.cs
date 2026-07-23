using FluentValidation;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.CreateAttendance;

public sealed class CreateStudentAttendanceValidator
    : AbstractValidator<CreateStudentAttendanceCommand>
{
    public CreateStudentAttendanceValidator()
    {
        RuleFor(x => x.TeachingAssignmentId)
            .GreaterThan(0)
            .WithMessage("Teaching Assignment is required.");

        RuleFor(x => x.AttendanceDate)
            .NotEmpty()
            .WithMessage("Attendance date is required.");

        RuleFor(x => x.Students)
            .NotNull()
            .WithMessage("Students are required.")
            .Must(x => x.Any())
            .WithMessage("At least one student is required.");

        RuleForEach(x => x.Students)
            .SetValidator(new CreateStudentAttendanceItemValidator());

        RuleFor(x => x.Students)
            .Must(HaveUniqueStudentEnrollments)
            .WithMessage("Duplicate Student Enrollment found in request.");
    }

    private static bool HaveUniqueStudentEnrollments(
        List<CreateStudentAttendanceItem> students)
    {
        if (students == null || students.Count == 0)
            return true;

        return students
            .Select(x => x.StudentEnrollmentId)
            .Distinct()
            .Count() == students.Count;
    }
}

public sealed class CreateStudentAttendanceItemValidator
    : AbstractValidator<CreateStudentAttendanceItem>
{
    public CreateStudentAttendanceItemValidator()
    {
        RuleFor(x => x.StudentEnrollmentId)
            .GreaterThan(0)
            .WithMessage("Student Enrollment is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid attendance status.");

        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}