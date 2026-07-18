using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Classes.DTOs;

namespace SchoolERP.Application.Features.Classes.Queries.GetClasses;

public sealed class GetClassesHandler
    : IRequestHandler<GetClassesQuery, PaginatedList<ClassDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClassesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ClassDto>> Handle(
        GetClassesQuery request,
        CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.classes
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();

            query = query.Where(x =>
                x.ClassName.Contains(search) ||
                x.ClassCode.Contains(search));
        }

        var totalRecords = await query.CountAsync(cancellationToken);

        // Sorting
        query = request.SortBy?.ToLower() switch
        {
            "classname" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.ClassName)
                : query.OrderBy(x => x.ClassName),

            "classcode" => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.ClassCode)
                : query.OrderBy(x => x.ClassCode),

            _ => request.SortDirection?.ToLower() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ClassDto
            {
                Id = x.Id,
                ClassName = x.ClassName,
                ClassCode = x.ClassCode,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<ClassDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}