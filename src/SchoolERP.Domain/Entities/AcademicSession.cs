using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class AcademicSession : BaseAuditableEntity
{
    public string SessionName { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public bool IsCurrent { get; private set; }

    public string? Description { get; private set; }
    public ICollection<StudentEnrollment> Enrollments { get; private set; }
        = new List<StudentEnrollment>();

    private AcademicSession()
    {
    }

    public AcademicSession(
        string sessionName,
        DateOnly startDate,
        DateOnly endDate,
        bool isCurrent,
        string? description)
    {
        SessionName = sessionName.Trim();
        StartDate = startDate;
        EndDate = endDate;
        IsCurrent = isCurrent;
        Description = description;
    }

    public void Update(
        string sessionName,
        DateOnly startDate,
        DateOnly endDate,
        bool isCurrent,
        string? description,
        bool isActive)
    {
        SessionName = sessionName.Trim();
        StartDate = startDate;
        EndDate = endDate;
        IsCurrent = isCurrent;
        Description = description;

        if (isActive)
            Activate();
        else
            Deactivate();

        MarkAsUpdated();
    }
    public void SetCurrent()
    {
        IsCurrent = true;
    }

    public void RemoveCurrent()
    {
        IsCurrent = false;
    }
}