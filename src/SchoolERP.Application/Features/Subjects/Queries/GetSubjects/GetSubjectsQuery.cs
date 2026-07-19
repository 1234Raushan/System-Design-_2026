using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Subjects.DTOs;

namespace SchoolERP.Application.Features.Subjects.Queries.GetSubjects;

public sealed record GetSubjectsQuery
    : PagedQuery,
      IRequest<PaginatedList<SubjectDto>>;