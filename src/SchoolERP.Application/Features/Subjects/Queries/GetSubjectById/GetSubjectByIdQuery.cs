using MediatR;
using SchoolERP.Application.Features.Subjects.DTOs;

namespace SchoolERP.Application.Features.Subjects.Queries.GetSubjectById;

public sealed record GetSubjectByIdQuery(int Id)
    : IRequest<SubjectDto>;