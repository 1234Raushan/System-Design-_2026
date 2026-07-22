namespace SchoolERP.Application.Features.TeachingAssignments.DTOs;

public sealed class UpdateTeachingAssignmentRequest
{
    public int Id { get; init; }

    public int TeacherId { get; init; }

    public int SubjectId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }

    public bool IsActive { get; init; }
}