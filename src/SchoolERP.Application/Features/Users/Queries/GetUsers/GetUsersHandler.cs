using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Users.DTOs;

namespace SchoolERP.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    private readonly IApplicationDbContext _context;

    public GetUsersHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _context.Users
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .Include(x => x.Role)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();
            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.Email.Contains(search) ||
                x.UserName.Contains(search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        query = request.SortBy?.ToLowerInvariant() switch
        {
            "firstname" => request.SortDirection?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(x => x.FirstName)
                : query.OrderBy(x => x.FirstName),
            "email" => request.SortDirection?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(x => x.Email)
                : query.OrderBy(x => x.Email),
            _ => request.SortDirection?.ToLowerInvariant() == "desc"
                ? query.OrderByDescending(x => x.CreatedDate)
                : query.OrderBy(x => x.CreatedDate)
        };

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive,
                RoleId = x.RoleId,
                RoleName = x.Role != null ? x.Role.RoleName : string.Empty
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<UserDto>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }
}
