using MediatR;
using SchoolERP.Application.Features.TeachingAssignments.DTOs;

namespace SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentById;

public sealed record GetTeachingAssignmentByIdQuery(int Id)
    : IRequest<TeachingAssignmentDto>;