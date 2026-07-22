using FluentValidation;
using SchoolERP.Application.Features.StudentAttendance.DTOs;
namespace SchoolERP.Application.Features.StudentAttendance.Commands.CreateAttendance;

public sealed class CreateStudentAttendanceValidator
    : AbstractValidator<CreateStudentAttendanceCommand>
{
    public CreateStudentAttendanceValidator()
    {
        RuleFor(x => x.AcademicSessionId)
            .GreaterThan(0)
            .WithMessage("Academic Session is required.");

        RuleFor(x => x.ClassId)
            .GreaterThan(0)
            .WithMessage("Class is required.");

        RuleFor(x => x.SectionId)
            .GreaterThan(0)
            .WithMessage("Section is required.");

        RuleFor(x => x.AttendanceDate)
            .NotEmpty()
            .WithMessage("Attendance date is required.");

        RuleFor(x => x.Students)
            .NotNull()
            .WithMessage("Students are required.")
            .Must(x => x.Any())
            .WithMessage("At least one student is required.");

        RuleForEach(x => x.Students)
            .SetValidator(new StudentAttendanceDtoValidator());

        RuleFor(x => x.Students)
            .Must(HaveUniqueStudentEnrollments)
            .WithMessage("Duplicate Student Enrollment found in request.");
    }

    private static bool HaveUniqueStudentEnrollments(
        List<StudentAttendanceDto> students)
    {
        if (students == null || students.Count == 0)
            return true;

        return students
            .Select(x => x.StudentEnrollmentId)
            .Distinct()
            .Count() == students.Count;
    }
}

public sealed class StudentAttendanceDtoValidator
    : AbstractValidator<StudentAttendanceDto>
{
    public StudentAttendanceDtoValidator()
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