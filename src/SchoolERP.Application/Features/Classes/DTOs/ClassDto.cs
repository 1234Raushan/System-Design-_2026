namespace SchoolERP.Application.Features.Classes.DTOs;

public sealed class ClassDto
{
    public int Id { get; set; }

    public string ClassName { get; set; } = string.Empty;

    public string ClassCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}