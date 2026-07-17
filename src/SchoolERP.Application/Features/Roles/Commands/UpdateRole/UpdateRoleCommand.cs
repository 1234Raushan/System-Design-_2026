using MediatR;

namespace SchoolERP.Application.Features.Roles.Commands.UpdateRole;

public sealed record UpdateRoleCommand(int Id, string Name, string? Description, bool IsActive) : IRequest<int>;
