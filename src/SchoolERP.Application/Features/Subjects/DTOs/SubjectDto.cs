namespace SchoolERP.Application.Features.Subjects.DTOs;

public sealed class SubjectDto
{
    public int Id { get; set; }

    public string SubjectName { get; set; } = string.Empty;

    public string SubjectCode { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}