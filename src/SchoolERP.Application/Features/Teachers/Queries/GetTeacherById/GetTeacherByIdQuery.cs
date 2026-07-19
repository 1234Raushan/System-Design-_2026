using MediatR;
using SchoolERP.Application.Features.Teachers.DTOs;

namespace SchoolERP.Application.Features.Teachers.Queries.GetTeacherById;

public sealed record GetTeacherByIdQuery(int Id)
    : IRequest<TeacherDto>;