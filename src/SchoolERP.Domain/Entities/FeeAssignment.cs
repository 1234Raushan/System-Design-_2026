using SchoolERP.Domain.Common;

namespace SchoolERP.Domain.Entities;

public sealed class FeeAssignment : BaseAuditableEntity
{
    public int StudentEnrollmentId { get; private set; }

    public int AcademicSessionId { get; private set; }


    public decimal TotalAmount { get; private set; }

    public decimal PaidAmount { get; private set; }

    public decimal DueAmount { get; private set; }



    // Navigation

    public StudentEnrollment StudentEnrollment
    {
        get; private set;
    } = null!;


    public AcademicSession AcademicSession
    {
        get; private set;
    } = null!;


    public ICollection<FeePayment> FeePayments
    {
        get; private set;
    }
    = new List<FeePayment>();



    private FeeAssignment()
    {
    }



    public FeeAssignment(
        int studentEnrollmentId,
        int academicSessionId,
        decimal totalAmount)
    {
        StudentEnrollmentId = studentEnrollmentId;

        AcademicSessionId = academicSessionId;

        TotalAmount = totalAmount;

        PaidAmount = 0;

        DueAmount = totalAmount;
    }



    public void AddPayment(decimal amount)
    {
        PaidAmount += amount;

        DueAmount =
            TotalAmount - PaidAmount;
    }



    public void Update(
        decimal totalAmount,
        bool isActive)
    {
        TotalAmount = totalAmount;


        DueAmount =
            TotalAmount - PaidAmount;



        if (isActive)
            Activate();
        else
            Deactivate();


        MarkAsUpdated();
    }
}