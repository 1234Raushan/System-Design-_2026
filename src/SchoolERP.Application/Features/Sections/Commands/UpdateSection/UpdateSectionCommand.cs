using MediatR;

namespace SchoolERP.Application.Features.Sections.Commands.UpdateSection;

public sealed record UpdateSectionCommand : IRequest
{
    public int Id { get; init; }

    public string SectionName { get; init; } = string.Empty;

    public int ClassId { get; init; }

    public bool IsActive { get; init; }
}