using MediatR;

namespace SchoolERP.Application.Features.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(string Name, string? Description) : IRequest<int>;
