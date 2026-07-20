using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.Attendance.Commands.CreateAttendance;

public sealed record CreateAttendanceCommand
    : IRequest<int>
{
    public int AcademicSessionId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }

    public DateOnly AttendanceDate { get; init; }


    public List<StudentAttendanceDto> Students { get; init; }
        = new();
}


public sealed record StudentAttendanceDto
{
    public int StudentId { get; init; }

    public AttendanceStatus Status { get; init; }

    public string? Remarks { get; init; }
}