using MediatR;

namespace SchoolERP.Application.Features.Classes.Commands.UpdateClass;

public sealed record UpdateClassCommand : IRequest
{
    public int Id { get; init; }

    public string ClassName { get; init; } = string.Empty;

    public string ClassCode { get; init; } = string.Empty;

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}