namespace SchoolERP.Application.Features.Sections.DTOs;

public sealed class SectionDto
{
    public int Id { get; set; }

    public string SectionName { get; set; } = string.Empty;

    public int ClassId { get; set; }

    public string ClassName { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}