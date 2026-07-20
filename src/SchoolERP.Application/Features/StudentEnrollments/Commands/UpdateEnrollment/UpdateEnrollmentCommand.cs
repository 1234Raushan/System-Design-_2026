using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.UpdateEnrollment;

public sealed record UpdateEnrollmentCommand : IRequest
{
    public int Id { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }

    public int RollNumber { get; init; }

    public EnrollmentStatus Status { get; init; }

    public string? Remarks { get; init; }

    public bool IsActive { get; init; }
}