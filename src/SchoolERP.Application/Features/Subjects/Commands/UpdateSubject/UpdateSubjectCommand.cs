using MediatR;

namespace SchoolERP.Application.Features.Subjects.Commands.UpdateSubject;

public sealed record UpdateSubjectCommand : IRequest
{
    public int Id { get; init; }

    public string SubjectName { get; init; } = string.Empty;

    public string SubjectCode { get; init; } = string.Empty;

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}