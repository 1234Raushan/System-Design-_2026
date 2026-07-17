using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Roles.DTOs;
using SchoolERP.Application.Features.Users.Queries.GetUserById;

namespace SchoolERP.Application.Features.Roles.Queries.GetRoles;

public sealed class GetRolesHandler : IRequestHandler<GetRolesQuery, PaginatedList<RoleDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRolesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Roles.AsNoTracking().Where(x => !x.IsDeleted).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();
            query = query.Where(x => x.RoleName.Contains(search));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = request.SortBy?.ToLowerInvariant() switch
        {
            "name" => request.SortDirection?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(x => x.RoleName)
                : query.OrderBy(x => x.RoleName),
            _ => request.SortDirection?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.RoleName,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<RoleDto>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }
}
