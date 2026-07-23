using SchoolERP.Domain.Common;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Domain.Entities;

public sealed class StudentEnrollment : BaseAuditableEntity
{
    public int StudentId { get; private set; }

    public int AcademicSessionId { get; private set; }

    public int ClassId { get; private set; }

    public int SectionId { get; private set; }

    public int RollNumber { get; private set; }

    public DateOnly AdmissionDate { get; private set; }

    public EnrollmentStatus Status { get; private set; }

    public string? Remarks { get; private set; }

    // Navigation Properties

    public Student Student { get; private set; } = null!;

    public AcademicSession AcademicSession { get; private set; } = null!;

    public Class_A Class { get; private set; } = null!;

    public Section Section { get; private set; } = null!;
    public ICollection<Student_Attendance> StudentAttendances { get; private set; }
    = new List<Student_Attendance>();

    public ICollection<Mark> Marks { get; private set; }
    = new List<Mark>();

    private StudentEnrollment()
    {
    }

    public StudentEnrollment(
        int studentId,
        int academicSessionId,
        int classId,
        int sectionId,
        int rollNumber,
        DateOnly admissionDate,
        EnrollmentStatus status,
        string? remarks)
    {
        StudentId = studentId;
        AcademicSessionId = academicSessionId;
        ClassId = classId;
        SectionId = sectionId;
        RollNumber = rollNumber;
        AdmissionDate = admissionDate;
        Status = status;
        Remarks = remarks;
    }

    public void Update(
        int classId,
        int sectionId,
        int rollNumber,
        EnrollmentStatus status,
        string? remarks,
        bool isActive)
    {
        ClassId = classId;
        SectionId = sectionId;
        RollNumber = rollNumber;
        Status = status;
        Remarks = remarks;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }

    public void Promote(
        int classId,
        int sectionId)
    {
        ClassId = classId;
        SectionId = sectionId;
        Status = EnrollmentStatus.Promoted;

        MarkAsUpdated();
    }
}