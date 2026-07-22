using MediatR;
using SchoolERP.Domain.Enums;
using SchoolERP.Application.Features.StudentAttendance.DTOs;

public sealed record CreateStudentAttendanceCommand
    : IRequest<int>
{
    public int AcademicSessionId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }

    public DateOnly AttendanceDate { get; init; }

    public List<StudentAttendanceDto> Students { get; init; }
        = new();
}
