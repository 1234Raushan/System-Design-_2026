using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;
using SchoolERP.Domain.Enums;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.PromoteStudent;

public sealed class PromoteStudentHandler
    : IRequestHandler<PromoteStudentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public PromoteStudentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(
        PromoteStudentCommand request,
        CancellationToken cancellationToken)
    {

        var currentEnrollment =
            await _context.StudentEnrollments
            .FirstOrDefaultAsync(x =>
                x.Id == request.StudentEnrollmentId &&
                !x.IsDeleted,
                cancellationToken);


        if (currentEnrollment is null)
            throw new KeyNotFoundException(
                "Current enrollment not found.");


        // Old enrollment promoted
        currentEnrollment.Promote(
            currentEnrollment.ClassId,
            currentEnrollment.SectionId);



        // Check duplicate enrollment
        var exists =
            await _context.StudentEnrollments.AnyAsync(x =>
                x.StudentId == currentEnrollment.StudentId &&
                x.AcademicSessionId == request.NewAcademicSessionId &&
                !x.IsDeleted,
                cancellationToken);


        if (exists)
            throw new InvalidOperationException(
                "Student already enrolled in this session.");



        // Create new enrollment

        var newEnrollment = new StudentEnrollment(
            currentEnrollment.StudentId,
            request.NewAcademicSessionId,
            request.NewClassId,
            request.NewSectionId,
            request.NewRollNumber,
            DateOnly.FromDateTime(DateTime.UtcNow),
            EnrollmentStatus.Active,
            "Promoted from previous class"
        );


        _context.StudentEnrollments.Add(newEnrollment);


        await _context.SaveChangesAsync(
            cancellationToken);


        return newEnrollment.Id;
    }
}