using MediatR;

namespace SchoolERP.Application.Features.AcademicSessions.Commands.UpdateAcademicSession;

public sealed record UpdateAcademicSessionCommand : IRequest
{
    public int Id { get; init; }

    public string SessionName { get; init; } = string.Empty;

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public bool IsCurrent { get; init; }

    public bool IsActive { get; init; }

    public string? Description { get; init; }
}