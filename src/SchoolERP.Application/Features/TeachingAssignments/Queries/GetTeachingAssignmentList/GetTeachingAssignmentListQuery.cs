using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.TeachingAssignments.DTOs;

namespace SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentList;

public sealed record GetTeachingAssignmentListQuery
    : PagedQuery,
      IRequest<PaginatedList<TeachingAssignmentDto>>;