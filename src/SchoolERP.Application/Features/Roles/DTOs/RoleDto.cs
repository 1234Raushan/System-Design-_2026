namespace SchoolERP.Application.Features.Roles.DTOs;

public sealed class RoleDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public bool IsDeleted { get; init; }
}
