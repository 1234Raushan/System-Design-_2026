using MediatR;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.CreateAcademicSession;

public sealed record CreateAcademicSessionCommand : IRequest<int>
{
    public string SessionName { get; init; } = string.Empty;

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public bool IsCurrent { get; init; }

    public string? Description { get; init; }
}