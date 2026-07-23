using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.StudentAttendance.DTOs;

namespace SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceList;

public sealed class GetStudentAttendanceListHandler
    : IRequestHandler<
        GetStudentAttendanceListQuery,
        PaginatedList<StudentAttendanceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStudentAttendanceListHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<StudentAttendanceDto>> Handle(
        GetStudentAttendanceListQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0
            ? 1
            : request.PageNumber;

        var pageSize = request.PageSize <= 0
            ? 10
            : request.PageSize;

        var query = _context.StudentAttendances
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.StudentEnrollment.Student.FirstName.Contains(search) ||
                x.StudentEnrollment.Student.LastName.Contains(search) ||
                x.StudentEnrollment.RollNumber.ToString().Contains(search));
        }

        // Filter by Attendance Session
        if (request.AttendanceSessionId.HasValue)
        {
            query = query.Where(x =>
                x.AttendanceSessionId ==
                request.AttendanceSessionId.Value);
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "rollnumber" =>
                request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x =>
                        x.StudentEnrollment.RollNumber)
                    : query.OrderBy(x =>
                        x.StudentEnrollment.RollNumber),

            "studentname" =>
                request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x =>
                        x.StudentEnrollment.Student.FirstName)
                    : query.OrderBy(x =>
                        x.StudentEnrollment.Student.FirstName),

            "status" =>
                request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.Status)
                    : query.OrderBy(x => x.Status),

            _ =>
                request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(x => x.CreatedDate)
                    : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new StudentAttendanceDto
            {
                Id = x.Id,
                AttendanceSessionId = x.AttendanceSessionId,
                StudentEnrollmentId = x.StudentEnrollmentId,
                RollNumber = x.StudentEnrollment.RollNumber,
                StudentName =
                    x.StudentEnrollment.Student.FirstName + " " +
                    x.StudentEnrollment.Student.LastName,
                AttendanceDate =
                    x.AttendanceSession.AttendanceDate,
                Status = x.Status,
                Remarks = x.Remarks
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<StudentAttendanceDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}