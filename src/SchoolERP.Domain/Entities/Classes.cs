using SchoolERP.Domain.Common;
using static System.Collections.Specialized.BitVector32;

namespace SchoolERP.Domain.Entities;

public sealed class Class_A : BaseAuditableEntity
{
    public string ClassName { get; private set; } = string.Empty;

    public string ClassCode { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    // Navigation
    //public ICollection<Student> Students { get; private set; }
    //    = new List<Student>();

    public ICollection<Section> Sections { get; private set; }
        = new List<Section>();

    public ICollection<TeacherClass> TeacherClasses { get; private set; }
        = new List<TeacherClass>();

    public ICollection<StudentEnrollment> Enrollments { get; private set; }
        = new List<StudentEnrollment>();

    private Class_A()
    {
    }

    public Class_A(
        string className,
        string classCode,
        string? description,
        bool isActive)
    {
        Update(className, classCode, description, isActive);
    }

    public void Update(
        string className,
        string classCode,
        string? description,bool isActive)
    {
        if (string.IsNullOrWhiteSpace(className))
            throw new ArgumentException("Class name is required.");

        if (string.IsNullOrWhiteSpace(classCode))
            throw new ArgumentException("Class code is required.");

        ClassName = className.Trim();
        ClassCode = classCode.Trim().ToUpper();
        Description = description;

        MarkAsUpdated();
    }
}