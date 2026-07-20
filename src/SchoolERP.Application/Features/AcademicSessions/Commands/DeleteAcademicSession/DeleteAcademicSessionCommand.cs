using MediatR;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.DeleteAcademicSession;

public sealed record DeleteAcademicSessionCommand(int Id) : IRequest;