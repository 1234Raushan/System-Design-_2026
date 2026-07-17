namespace SchoolERP.Application.Features.Roles.DTOs;

public sealed class UpdateRoleRequest
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
}
