using MediatR;
using SchoolERP.Application.Features.AcademicSessions.DTOs;

namespace SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessionById;

public sealed record GetAcademicSessionByIdQuery(int Id)
    : IRequest<AcademicSessionDto>;