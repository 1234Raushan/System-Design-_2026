using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Fees.Commands.CreateFeeAssignment;

public sealed class CreateFeeAssignmentHandler
    : IRequestHandler<CreateFeeAssignmentCommand, int>
{
    private readonly IApplicationDbContext _context;


    public CreateFeeAssignmentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<int> Handle(
        CreateFeeAssignmentCommand request,
        CancellationToken cancellationToken)
    {

        // Student Enrollment Check

        var studentExists =
            await _context.StudentEnrollments
            .AnyAsync(x =>
                x.Id == request.StudentEnrollmentId &&
                !x.IsDeleted,
                cancellationToken);


        if (!studentExists)
        {
            throw new InvalidOperationException(
                "Student enrollment not found.");
        }



        // Duplicate Fee Assignment Check

        var alreadyExists =
            await _context.FeeAssignments
            .AnyAsync(x =>
                x.StudentEnrollmentId == request.StudentEnrollmentId &&
                x.AcademicSessionId == request.AcademicSessionId &&
                !x.IsDeleted,
                cancellationToken);



        if (alreadyExists)
        {
            throw new InvalidOperationException(
                "Fee already assigned for this student.");
        }



        // Create Fee Assignment

        var feeAssignment = new FeeAssignment(
            request.StudentEnrollmentId,
            request.AcademicSessionId,
            request.TotalAmount);



        _context.FeeAssignments.Add(feeAssignment);



        await _context.SaveChangesAsync(
            cancellationToken);



        return feeAssignment.Id;
    }
}