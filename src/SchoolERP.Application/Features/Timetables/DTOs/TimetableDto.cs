namespace SchoolERP.Application.Features.Timetables.DTOs;

public sealed class TimetableDto
{
    public int Id { get; set; }

    public int TeachingAssignmentId { get; set; }


    // Teaching Assignment Details

    public int TeacherId { get; set; }

    public string? TeacherName { get; set; }


    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }


    public int ClassId { get; set; }

    public string? ClassName { get; set; }


    public int SectionId { get; set; }

    public string? SectionName { get; set; }



    // Timetable Details

    public DayOfWeek DayOfWeek { get; set; }

    public int PeriodNumber { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string? RoomNumber { get; set; }

    public string? Remarks { get; set; }


    public bool IsActive { get; set; }
}