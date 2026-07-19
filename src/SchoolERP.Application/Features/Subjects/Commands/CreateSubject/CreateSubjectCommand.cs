using MediatR;

namespace SchoolERP.Application.Features.Subjects.Commands.CreateSubject;

public sealed record CreateSubjectCommand : IRequest<int>
{
    public string SubjectName { get; init; } = string.Empty;

    public string SubjectCode { get; init; } = string.Empty;

    public string? Description { get; init; }
    public bool IsActive { get; init; }
}