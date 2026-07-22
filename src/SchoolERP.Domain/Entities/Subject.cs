using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Subject : BaseAuditableEntity
{
    public string SubjectName { get; private set; } = string.Empty;

    public string SubjectCode { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public ICollection<TeacherSubject> TeacherSubjects { get; private set; }
    = new List<TeacherSubject>();
    public ICollection<TeachingAssignment> TeachingAssignments { get; private set; }
        = new List<TeachingAssignment>();

    private Subject()
    {
    }

    public Subject(
        string subjectName,
        string subjectCode,
        string? description,
        bool IsActive)
    {
        Update(subjectName, subjectCode, description, IsActive);
    }

    public void Update(
        string subjectName,
        string subjectCode,
        string? description,
        bool IsActive)
    {
        if (string.IsNullOrWhiteSpace(subjectName))
            throw new ArgumentException("Subject name is required.");

        if (string.IsNullOrWhiteSpace(subjectCode))
            throw new ArgumentException("Subject code is required.");

        SubjectName = subjectName.Trim();
        SubjectCode = subjectCode.Trim().ToUpperInvariant();
        Description = description;

        MarkAsUpdated();
    }
}