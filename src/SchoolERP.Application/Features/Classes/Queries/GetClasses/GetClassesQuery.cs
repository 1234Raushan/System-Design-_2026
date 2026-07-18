using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Classes.DTOs;

namespace SchoolERP.Application.Features.Classes.Queries.GetClasses;

public sealed record GetClassesQuery
    : PagedQuery,
      IRequest<PaginatedList<ClassDto>>;