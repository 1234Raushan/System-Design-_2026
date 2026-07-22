using MediatR;
using SchoolERP.Application.Features.StudentAttendance.DTOs;
namespace SchoolERP.Application.Features.StudentAttendance.Queries.GetStudentAttendanceList;
using StudentAttendanceEntity = SchoolERP.Domain.Entities.Student_Attendance;
public sealed record GetStudentAttendanceListQuery
    : IRequest<List<StudentAttendanceDto>>
{
    public int? AcademicSessionId { get; init; }

    public int? ClassId { get; init; }

    public int? SectionId { get; init; }

    public DateOnly? AttendanceDate { get; init; }

    public int? StudentEnrollmentId { get; init; }
}