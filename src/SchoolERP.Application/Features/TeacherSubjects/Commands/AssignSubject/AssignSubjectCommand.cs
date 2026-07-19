using MediatR;

namespace SchoolERP.Application.Features.TeacherSubjects.Commands.AssignSubject;

public sealed record AssignSubjectCommand : IRequest<int>
{
    public int TeacherId { get; init; }

    public int SubjectId { get; init; }
}