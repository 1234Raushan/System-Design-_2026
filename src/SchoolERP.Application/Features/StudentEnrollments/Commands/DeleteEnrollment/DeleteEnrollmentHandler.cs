using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.DeleteEnrollment;

public sealed class DeleteEnrollmentHandler
    : IRequestHandler<DeleteEnrollmentCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEnrollmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var enrollment = await _context.StudentEnrollments
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (enrollment is null)
            throw new KeyNotFoundException("Enrollment not found.");

        enrollment.SoftDelete();   // BaseAuditableEntity method

        await _context.SaveChangesAsync(cancellationToken);
    }
}