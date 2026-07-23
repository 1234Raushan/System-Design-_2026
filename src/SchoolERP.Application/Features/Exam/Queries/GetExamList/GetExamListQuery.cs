using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Exams.DTOs;

namespace SchoolERP.Application.Features.Exams.Queries.GetExamList;

public sealed record GetExamListQuery
    : PagedQuery,
      IRequest<PaginatedList<ExamDto>>;