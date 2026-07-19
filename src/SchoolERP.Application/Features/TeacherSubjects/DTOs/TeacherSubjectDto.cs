namespace SchoolERP.Application.Features.TeacherSubjects.DTOs.GetTeacherSubjects;

public sealed class TeacherSubjectDto
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = string.Empty;

    public string SubjectCode { get; set; } = string.Empty;
}