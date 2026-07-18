using MediatR;
using SchoolERP.Application.Features.Classes.DTOs;

namespace SchoolERP.Application.Features.Classes.Queries.GetClassById;

public sealed record GetClassByIdQuery(int Id)
    : IRequest<ClassDto>;