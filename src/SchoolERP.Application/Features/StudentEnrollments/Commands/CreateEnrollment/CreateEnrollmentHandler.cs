using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.StudentEnrollments.Commands.CreateEnrollment;

public sealed class CreateEnrollmentHandler
    : IRequestHandler<CreateEnrollmentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEnrollmentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        // Student Exists
        var studentExists = await _context.Students
            .AnyAsync(x => x.Id == request.StudentId, cancellationToken);

        if (!studentExists)
            throw new InvalidOperationException("Student not found.");

        // Academic Session Exists
        var sessionExists = await _context.AcademicSessions
            .AnyAsync(x => x.Id == request.AcademicSessionId, cancellationToken);

        if (!sessionExists)
            throw new InvalidOperationException("Academic Session not found.");

        // Class Exists
        var classExists = await _context.classes
            .AnyAsync(x => x.Id == request.ClassId, cancellationToken);

        if (!classExists)
            throw new InvalidOperationException("Class not found.");

        // Section Exists
        var section = await _context.Sections
            .FirstOrDefaultAsync(x => x.Id == request.SectionId, cancellationToken);

        if (section is null)
            throw new InvalidOperationException("Section not found.");

        // Section belongs to selected Class
        if (section.ClassId != request.ClassId)
            throw new InvalidOperationException("Selected Section does not belong to selected Class.");

        // Duplicate Roll Number
        var rollExists = await _context.StudentEnrollments.AnyAsync(x =>
            x.AcademicSessionId == request.AcademicSessionId &&
            x.ClassId == request.ClassId &&
            x.SectionId == request.SectionId &&
            x.RollNumber == request.RollNumber &&
            !x.IsDeleted,
            cancellationToken);

        if (rollExists)
            throw new InvalidOperationException("Roll Number already exists.");

        // Student already enrolled in same Academic Session
        var alreadyEnrolled = await _context.StudentEnrollments.AnyAsync(x =>
            x.StudentId == request.StudentId &&
            x.AcademicSessionId == request.AcademicSessionId &&
            !x.IsDeleted,
            cancellationToken);

        if (alreadyEnrolled)
            throw new InvalidOperationException("Student is already enrolled in this Academic Session.");

        var enrollment = new StudentEnrollment(
            request.StudentId,
            request.AcademicSessionId,
            request.ClassId,
            request.SectionId,
            request.RollNumber,
            request.AdmissionDate,
            request.Status,
            request.Remarks);

        _context.StudentEnrollments.Add(enrollment);

        await _context.SaveChangesAsync(cancellationToken);

        return enrollment.Id;
    }
}