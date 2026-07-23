using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Exams.DTOs;

namespace SchoolERP.Application.Features.Exams.Queries.GetExamList;

public sealed class GetExamListHandler
    : IRequestHandler<GetExamListQuery, PaginatedList<ExamDto>>
{
    private readonly IApplicationDbContext _context;

    public GetExamListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ExamDto>> Handle(
        GetExamListQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Exams
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

                x.ExamName.Contains(search) ||

                x.ExamType.Contains(search) ||

                x.TeachingAssignment.Teacher.FirstName.Contains(search) ||

                x.TeachingAssignment.Teacher.LastName.Contains(search) ||

                x.TeachingAssignment.Subject.SubjectName.Contains(search) ||

                x.TeachingAssignment.Class.ClassName.Contains(search) ||

                x.TeachingAssignment.Section.SectionName.Contains(search));
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // Sorting

        query = request.SortBy?.ToLower() switch
        {
            "examname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.ExamName)
                : query.OrderBy(x => x.ExamName),

            "examdate" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.ExamDate)
                : query.OrderBy(x => x.ExamDate),

            "teachername" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.TeachingAssignment.Teacher.FirstName)
                : query.OrderBy(x => x.TeachingAssignment.Teacher.FirstName),

            "subjectname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.TeachingAssignment.Subject.SubjectName)
                : query.OrderBy(x => x.TeachingAssignment.Subject.SubjectName),

            "classname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.TeachingAssignment.Class.ClassName)
                : query.OrderBy(x => x.TeachingAssignment.Class.ClassName),

            _ => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ExamDto
            {
                Id = x.Id,

                TeachingAssignmentId = x.TeachingAssignmentId,

                TeacherName =
                    x.TeachingAssignment.Teacher.FirstName + " " +
                    x.TeachingAssignment.Teacher.LastName,

                SubjectName =
                    x.TeachingAssignment.Subject.SubjectName,

                ClassName =
                    x.TeachingAssignment.Class.ClassName,

                SectionName =
                    x.TeachingAssignment.Section.SectionName,

                ExamName = x.ExamName,

                ExamType = x.ExamType,

                ExamDate = x.ExamDate,

                MaximumMarks = x.MaximumMarks,

                PassingMarks = x.PassingMarks,

                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<ExamDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}