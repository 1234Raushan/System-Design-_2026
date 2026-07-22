using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentAttendance.Commands.UpdateAttendance;

public sealed record UpdateStudentAttendanceCommand
    : IRequest
{
    public int AttendanceSessionId { get; init; }

    public List<UpdateStudentAttendanceDto> Students { get; init; }
        = new();
}

public sealed record UpdateStudentAttendanceDto
{
    public int StudentEnrollmentId { get; init; }

    public AttendanceStatus Status { get; init; }

    public string? Remarks { get; init; }
}