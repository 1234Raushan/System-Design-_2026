using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Sections.DTOs;

namespace SchoolERP.Application.Features.Sections.Queries.GetSections;

public sealed record GetSectionsQuery
    : PagedQuery,
      IRequest<PaginatedList<SectionDto>>;