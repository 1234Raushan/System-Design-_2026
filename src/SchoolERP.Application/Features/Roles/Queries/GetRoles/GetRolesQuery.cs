using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Roles.DTOs;

namespace SchoolERP.Application.Features.Roles.Queries.GetRoles;

//public sealed record GetRolesQuery(
//    int PageNumber,
//    int PageSize,
//    string? SearchTerm,
//    string? SortBy,
//    string? SortDirection,
//    bool? IsActive) : IRequest<PagedResult<RoleDto>>;

public sealed record GetRolesQuery
    : PagedQuery,
      IRequest<PaginatedList<RoleDto>>
{
    public bool? IsActive { get; init; }
}
