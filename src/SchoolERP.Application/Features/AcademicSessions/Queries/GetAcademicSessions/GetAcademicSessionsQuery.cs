using MediatR;
using SchoolERP.Application.Features.AcademicSessions.DTOs;

namespace SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessions;

public sealed record GetAcademicSessionsQuery
    : IRequest<List<AcademicSessionDto>>;