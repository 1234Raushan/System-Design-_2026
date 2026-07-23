using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.StudentAttendance.DTOs;

namespace SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceList;

public sealed record GetStudentAttendanceListQuery
    : PagedQuery,
      IRequest<PaginatedList<StudentAttendanceDto>>
{
    public int? AttendanceSessionId { get; init; }
}