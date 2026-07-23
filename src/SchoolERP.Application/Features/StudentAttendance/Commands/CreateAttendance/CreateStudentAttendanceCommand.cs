using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.CreateAttendance;

public sealed record CreateStudentAttendanceCommand : IRequest<int>
{
    public int TeachingAssignmentId { get; init; }

    public DateOnly AttendanceDate { get; init; }

    public List<CreateStudentAttendanceItem> Students { get; init; } = [];
}

public sealed record CreateStudentAttendanceItem
{
    public int StudentEnrollmentId { get; init; }

    public AttendanceStatus Status { get; init; }

    public string? Remarks { get; init; }
}