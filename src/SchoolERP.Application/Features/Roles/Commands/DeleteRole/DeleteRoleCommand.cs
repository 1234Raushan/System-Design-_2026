using MediatR;

namespace SchoolERP.Application.Features.Roles.Commands.DeleteRole;

public sealed record DeleteRoleCommand(int Id) : IRequest<int>;
