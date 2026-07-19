using MediatR;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Teachers.DTOs;

namespace SchoolERP.Application.Features.Teachers.Queries.GetTeachers;

public sealed record GetTeachersQuery
    : PagedQuery,
      IRequest<PaginatedList<TeacherDto>>;