using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class Section : BaseAuditableEntity
{
    public string SectionName { get; private set; } = string.Empty;

    public int ClassId { get; private set; }

    public Class_A Class_A { get; private set; } = null!;

    public ICollection<Student> Students { get; private set; }
        = new List<Student>();

    private Section()
    {
    }

    public Section(
        string sectionName,
        int classId)
    {
        Update(sectionName, classId);
    }

    public void Update(
        string sectionName,
        int classId)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
            throw new ArgumentException("Section name is required.");

        if (classId <= 0)
            throw new ArgumentException("Invalid class.");

        SectionName = sectionName.Trim().ToUpperInvariant();
        ClassId = classId;

        MarkAsUpdated();
    }
}