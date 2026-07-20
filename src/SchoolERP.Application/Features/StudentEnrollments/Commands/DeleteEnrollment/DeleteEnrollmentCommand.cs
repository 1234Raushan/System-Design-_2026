using MediatR;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.DeleteEnrollment;

public sealed record DeleteEnrollmentCommand(int Id) : IRequest;