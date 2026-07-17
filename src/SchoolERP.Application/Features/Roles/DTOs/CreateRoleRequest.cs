namespace SchoolERP.Application.Features.Roles.DTOs;

public sealed class CreateRoleRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}
