using MediatR;
using SchoolERP.Application.Features.Roles.DTOs;

namespace SchoolERP.Application.Features.Roles.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(int Id) : IRequest<RoleDto?>;
