using MediatR;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.PromoteStudent;

public sealed record PromoteStudentCommand : IRequest<int>
{
    public int StudentEnrollmentId { get; init; }

    public int NewAcademicSessionId { get; init; }

    public int NewClassId { get; init; }

    public int NewSectionId { get; init; }

    public int NewRollNumber { get; init; }
}