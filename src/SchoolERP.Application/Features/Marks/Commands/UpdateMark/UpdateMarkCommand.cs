using MediatR;

namespace SchoolERP.Application.Features.Marks.Commands.UpdateMark;

public sealed record UpdateMarkCommand
    : IRequest
{
    public int Id { get; init; }

    public decimal ObtainedMarks { get; init; }

    public string? Remarks { get; init; }
}