using MediatR;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeeAssignment;

public sealed record CreateFeeAssignmentCommand
    : IRequest<int>
{
    public int StudentEnrollmentId { get; init; }

    public int AcademicSessionId { get; init; }

    public decimal TotalAmount { get; init; }
}