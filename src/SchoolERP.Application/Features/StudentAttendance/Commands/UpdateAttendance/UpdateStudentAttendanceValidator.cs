using FluentValidation;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.UpdateAttendance;

public sealed class UpdateStudentAttendanceValidator
    : AbstractValidator<UpdateStudentAttendanceCommand>
{
    public UpdateStudentAttendanceValidator()
    {
        RuleFor(x => x.AttendanceSessionId)
            .GreaterThan(0);

        RuleFor(x => x.Students)
            .NotEmpty();

        RuleForEach(x => x.Students)
            .SetValidator(new UpdateStudentAttendanceDtoValidator());

        RuleFor(x => x.Students)
            .Must(HaveUniqueStudentEnrollments)
            .WithMessage("Duplicate Student Enrollment found.");
    }

    private static bool HaveUniqueStudentEnrollments(
        List<UpdateStudentAttendanceDto> students)
    {
        return students
            .Select(x => x.StudentEnrollmentId)
            .Distinct()
            .Count() == students.Count;
    }
}

public sealed class UpdateStudentAttendanceDtoValidator
    : AbstractValidator<UpdateStudentAttendanceDto>
{
    public UpdateStudentAttendanceDtoValidator()
    {
        RuleFor(x => x.StudentEnrollmentId)
            .GreaterThan(0);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Remarks)
            .MaximumLength(500);
    }
}