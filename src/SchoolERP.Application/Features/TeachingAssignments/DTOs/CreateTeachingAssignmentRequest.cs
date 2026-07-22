namespace SchoolERP.Application.Features.TeachingAssignments.DTOs;

public sealed class CreateTeachingAssignmentRequest
{
    public int TeacherId { get; init; }

    public int SubjectId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }
}