using MediatR;

namespace SchoolERP.Application.Features.Classes.Commands.CreateClass;

public sealed record CreateClassCommand : IRequest<int>
{
    public string ClassName { get; init; } = string.Empty;

    public string ClassCode { get; init; } = string.Empty;

    public string? Description { get; init; }
    public bool isActive { get; init; } = true;
}