using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceById;

public sealed record GetStudentAttendanceByIdQuery(int AttendanceSessionId)
    : IRequest<AttendanceDetailsDto>;

public sealed class AttendanceDetailsDto
{
    public int AttendanceSessionId { get; set; }

    public int TeachingAssignmentId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public List<StudentAttendanceDetailsDto> Students { get; set; }
        = new();
}

public sealed class StudentAttendanceDetailsDto
{
    public int StudentEnrollmentId { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public int RollNumber { get; set; }

    public AttendanceStatus Status { get; set; }

    public string? Remarks { get; set; }
}