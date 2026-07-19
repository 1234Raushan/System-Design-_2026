namespace SchoolERP.Application.Features.TeacherClasses.Queries.GetTeacherClasses;

public sealed class TeacherClassDto
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public string ClassName { get; set; } = string.Empty;

    public string ClassCode { get; set; } = string.Empty;
}