using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.TeachingAssignments.DTOs;

namespace SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentList;

public sealed class GetTeachingAssignmentListHandler
    : IRequestHandler<GetTeachingAssignmentListQuery, PaginatedList<TeachingAssignmentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeachingAssignmentListHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<PaginatedList<TeachingAssignmentDto>> Handle(
        GetTeachingAssignmentListQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0
            ? 1
            : request.PageNumber;

        var pageSize = request.PageSize <= 0
            ? 10
            : request.PageSize;


        var query = _context.TeachingAssignments
            .AsNoTracking()
            .Include(x => x.Teacher)
            .Include(x => x.Subject)
            .Include(x => x.Class)
            .Include(x => x.Section)
            .Where(x => !x.IsDeleted)
            .AsQueryable();


        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.Teacher.FirstName.Contains(search) ||
                x.Teacher.LastName.Contains(search) ||
                x.Subject.SubjectName.Contains(search) ||
                x.Class.ClassName.Contains(search) ||
                x.Section.SectionName.Contains(search));
        }


        var totalRecords = await query
            .CountAsync(cancellationToken);



        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "teachername" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x =>
                    x.Teacher.FirstName)
                :
                query.OrderBy(x =>
                    x.Teacher.FirstName),


            "subjectname" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x =>
                    x.Subject.SubjectName)
                :
                query.OrderBy(x =>
                    x.Subject.SubjectName),


            "classname" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x =>
                    x.Class.ClassName)
                :
                query.OrderBy(x =>
                    x.Class.ClassName),


            _ => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x =>
                    x.CreatedDate)
                :
                query.OrderBy(x =>
                    x.CreatedDate)
        };


        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TeachingAssignmentDto
            {
                Id = x.Id,

                TeacherId = x.TeacherId,
                TeacherName =
                    x.Teacher.FirstName + " " +
                    x.Teacher.LastName,

                SubjectId = x.SubjectId,
                SubjectName = x.Subject.SubjectName,

                ClassId = x.ClassId,
                ClassName = x.Class.ClassName,

                SectionId = x.SectionId,
                SectionName = x.Section.SectionName,

                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);


        return new PaginatedList<TeachingAssignmentDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}