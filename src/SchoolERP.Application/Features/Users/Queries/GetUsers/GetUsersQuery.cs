using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Users.DTOs;

namespace SchoolERP.Application.Features.Users.Queries.GetUsers;

//public sealed record GetUsersQuery(
//    int PageNumber,
//    int PageSize,
//    string? SearchTerm,
//    string? SortBy,
//    string? SortDirection) : IRequest<PagedResult<UserDto>>;

public sealed record GetUsersQuery
    : PagedQuery,
      IRequest<PaginatedList<UserDto>>;