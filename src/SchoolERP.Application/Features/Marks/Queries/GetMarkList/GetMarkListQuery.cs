using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Marks.Queries.GetMarkById;

namespace SchoolERP.Application.Features.Marks.Queries.GetMarkList;

public sealed record GetMarkListQuery
    : PagedQuery,
      IRequest<PaginatedList<MarkListDto>>;