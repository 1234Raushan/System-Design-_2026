namespace SchoolERP.Application.Features.Marks.Queries.GetMarkById;

public sealed class MarkListDto
{
    public int Id { get; set; }

    public int ExamId { get; set; }

    public string ExamName { get; set; } = string.Empty;


    public int StudentEnrollmentId { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = string.Empty;


    public decimal ObtainedMarks { get; set; }

    public decimal MaximumMarks { get; set; }

    public decimal PassingMarks { get; set; }


    public string? Remarks { get; set; }
}