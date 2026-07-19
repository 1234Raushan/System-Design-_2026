using MediatR;

namespace SchoolERP.Application.Features.Sections.Commands.CreateSection;

public sealed record CreateSectionCommand : IRequest<int>
{
    public string SectionName { get; init; } = string.Empty;

    public int ClassId { get; init; }
}