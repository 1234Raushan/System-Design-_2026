using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.UpdateEnrollment;

public sealed class UpdateEnrollmentHandler
    : IRequestHandler<UpdateEnrollmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEnrollmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var enrollment = await _context.StudentEnrollments
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (enrollment is null)
            throw new KeyNotFoundException("Enrollment not found.");

        var classExists = await _context.classes
            .AnyAsync(x => x.Id == request.ClassId, cancellationToken);

        if (!classExists)
            throw new InvalidOperationException("Class not found.");

        var section = await _context.Sections
            .FirstOrDefaultAsync(x =>
                x.Id == request.SectionId,
                cancellationToken);

        if (section is null)
            throw new InvalidOperationException("Section not found.");

        if (section.ClassId != request.ClassId)
            throw new InvalidOperationException("Section does not belong to selected class.");

        var duplicateRoll = await _context.StudentEnrollments
            .AnyAsync(x =>
                x.Id != request.Id &&
                x.AcademicSessionId == enrollment.AcademicSessionId &&
                x.ClassId == request.ClassId &&
                x.SectionId == request.SectionId &&
                x.RollNumber == request.RollNumber &&
                !x.IsDeleted,
                cancellationToken);

        if (duplicateRoll)
            throw new InvalidOperationException("Roll number already exists.");

        enrollment.Update(
            request.ClassId,
            request.SectionId,
            request.RollNumber,
            request.Status,
            request.Remarks,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}