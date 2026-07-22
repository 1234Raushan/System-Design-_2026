using MediatR;

namespace SchoolERP.Application.Features.TeachingAssignments.Commands.CreateTeachingAssignment;

public sealed record CreateTeachingAssignmentCommand : IRequest<int>
{
    public int TeacherId { get; init; }

    public int SubjectId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }
}