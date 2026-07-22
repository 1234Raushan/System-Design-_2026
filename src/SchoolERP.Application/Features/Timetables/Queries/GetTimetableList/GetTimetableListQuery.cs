using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Timetables.DTOs;

namespace SchoolERP.Application.Features.Timetables.Queries.GetTimetableList;

public sealed record GetTimetableListQuery
    : PagedQuery,
      IRequest<PaginatedList<TimetableDto>>;