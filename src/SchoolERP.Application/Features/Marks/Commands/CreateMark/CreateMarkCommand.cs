using MediatR;

namespace SchoolERP.Application.Features.Marks.Commands.CreateMark;

public sealed record CreateMarkCommand
    : IRequest<int>
{
    public int ExamId { get; init; }

    public List<CreateMarkItem> Students { get; init; }
        = [];
}


public sealed record CreateMarkItem
{
    public int StudentEnrollmentId { get; init; }

    public decimal ObtainedMarks { get; init; }

    public string? Remarks { get; init; }
}