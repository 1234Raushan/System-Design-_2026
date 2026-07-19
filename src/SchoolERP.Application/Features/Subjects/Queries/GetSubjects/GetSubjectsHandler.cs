using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Subjects.DTOs;

namespace SchoolERP.Application.Features.Subjects.Queries.GetSubjects;

public sealed class GetSubjectsHandler
    : IRequestHandler<GetSubjectsQuery, PaginatedList<SubjectDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSubjectsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<SubjectDto>> Handle(
        GetSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Subjects
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.SubjectName.Contains(search) ||
                x.SubjectCode.Contains(search));
        }

        var totalRecords = await query.CountAsync(cancellationToken);

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "subjectname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.SubjectName)
                : query.OrderBy(x => x.SubjectName),

            "subjectcode" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.SubjectCode)
                : query.OrderBy(x => x.SubjectCode),

            _ => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new SubjectDto
            {
                Id = x.Id,
                SubjectName = x.SubjectName,
                SubjectCode = x.SubjectCode,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<SubjectDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}