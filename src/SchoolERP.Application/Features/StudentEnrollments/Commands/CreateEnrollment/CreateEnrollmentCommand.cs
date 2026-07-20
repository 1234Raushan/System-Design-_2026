using MediatR;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.CreateEnrollment;

public sealed record CreateEnrollmentCommand : IRequest<int>
{
    public int StudentId { get; init; }

    public int AcademicSessionId { get; init; }

    public int ClassId { get; init; }

    public int SectionId { get; init; }

    public int RollNumber { get; init; }

    public DateOnly AdmissionDate { get; init; }

    public EnrollmentStatus Status { get; init; }

    public string? Remarks { get; init; }
}