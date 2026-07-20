using MediatR;
using SchoolERP.Application.Features.StudentEnrollments.DTOs;

namespace SchoolERP.Application.Features.StudentEnrollments.Queries.GetEnrollmentById;

public sealed record GetEnrollmentByIdQuery(int Id)
    : IRequest<EnrollmentDto>;