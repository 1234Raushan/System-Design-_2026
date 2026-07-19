namespace SchoolERP.Application.Features.AcademicSessions.DTOs;

public sealed class AcademicSessionDto
{
    public int Id { get; set; }

    public string SessionName { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsCurrent { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}