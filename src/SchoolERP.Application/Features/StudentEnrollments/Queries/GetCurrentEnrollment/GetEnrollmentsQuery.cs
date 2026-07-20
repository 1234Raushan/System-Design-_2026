using MediatR;
using SchoolERP.Application.Features.StudentEnrollments.DTOs;

namespace SchoolERP.Application.Features.StudentEnrollments.Queries.GetEnrollments;

public sealed record GetEnrollmentsQuery : IRequest<IReadOnlyList<EnrollmentDto>>
{
    public int? AcademicSessionId { get; init; }

    public int? ClassId { get; init; }

    public int? SectionId { get; init; }

    public string? Search { get; init; }

    public bool? IsActive { get; init; }
}