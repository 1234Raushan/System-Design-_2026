using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Sections.DTOs;

namespace SchoolERP.Application.Features.Sections.Queries.GetSections;

public sealed class GetSectionsHandler
    : IRequestHandler<GetSectionsQuery, PaginatedList<SectionDto>>
{
    private readonly IApplicationDbContext _context;

    public GetSectionsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<SectionDto>> Handle(
        GetSectionsQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Sections
            .AsNoTracking()
            .Include(x => x.Class)
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.SectionName.Contains(search) ||
                x.Class.ClassName.Contains(search));
        }

        var totalRecords = await query.CountAsync(cancellationToken);

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "sectionname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.SectionName)
                : query.OrderBy(x => x.SectionName),

            "classname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.Class.ClassName)
                : query.OrderBy(x => x.Class.ClassName),

            _ => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new SectionDto
            {
                Id = x.Id,
                SectionName = x.SectionName,
                ClassId = x.ClassId,
                ClassName = x.Class.ClassName,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<SectionDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}