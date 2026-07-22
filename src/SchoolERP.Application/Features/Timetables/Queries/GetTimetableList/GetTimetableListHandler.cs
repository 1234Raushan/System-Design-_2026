using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Timetables.DTOs;

namespace SchoolERP.Application.Features.Timetables.Queries.GetTimetableList;

public sealed class GetTimetableListHandler
    : IRequestHandler<GetTimetableListQuery, PaginatedList<TimetableDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTimetableListHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<PaginatedList<TimetableDto>> Handle(
        GetTimetableListQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0
            ? 1
            : request.PageNumber;

        var pageSize = request.PageSize <= 0
            ? 10
            : request.PageSize;


        var query = _context.Timetables
            .AsNoTracking()
            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Teacher)
            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Subject)
            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Class)
            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Section)
            .Where(x => !x.IsDeleted)
            .AsQueryable();



        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                (x.RoomNumber != null &&
                 x.RoomNumber.Contains(search))

                ||

                (x.Remarks != null &&
                 x.Remarks.Contains(search))

                ||

                x.TeachingAssignment.Teacher.FirstName.Contains(search)

                ||

                x.TeachingAssignment.Subject.SubjectName.Contains(search)

                ||

                x.TeachingAssignment.Class.ClassName.Contains(search)

                ||

                x.TeachingAssignment.Section.SectionName.Contains(search)
            );
        }


        var totalRecords = await query
            .CountAsync(cancellationToken);



        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "dayofweek" =>
                request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.DayOfWeek)
                :
                query.OrderBy(x => x.DayOfWeek),


            "periodnumber" =>
                request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.PeriodNumber)
                :
                query.OrderBy(x => x.PeriodNumber),


            "starttime" =>
                request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.StartTime)
                :
                query.OrderBy(x => x.StartTime),


            _ =>
                request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.CreatedDate)
                :
                query.OrderBy(x => x.CreatedDate)
        };



        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TimetableDto
            {
                Id = x.Id,

                TeachingAssignmentId =
                    x.TeachingAssignmentId,


                TeacherId =
                    x.TeachingAssignment.TeacherId,

                TeacherName =
                    x.TeachingAssignment.Teacher.FirstName
                    + " "
                    + x.TeachingAssignment.Teacher.LastName,


                SubjectId =
                    x.TeachingAssignment.SubjectId,

                SubjectName =
                    x.TeachingAssignment.Subject.SubjectName,


                ClassId =
                    x.TeachingAssignment.ClassId,

                ClassName =
                    x.TeachingAssignment.Class.ClassName,


                SectionId =
                    x.TeachingAssignment.SectionId,

                SectionName =
                    x.TeachingAssignment.Section.SectionName,


                DayOfWeek = x.DayOfWeek,

                PeriodNumber = x.PeriodNumber,

                StartTime = x.StartTime,

                EndTime = x.EndTime,

                RoomNumber = x.RoomNumber,

                Remarks = x.Remarks,

                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);



        return new PaginatedList<TimetableDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}