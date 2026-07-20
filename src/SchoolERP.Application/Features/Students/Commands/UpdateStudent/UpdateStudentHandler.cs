using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Students.Commands.UpdateStudent;

public sealed class UpdateStudentHandler
    : IRequestHandler<UpdateStudentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateStudentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && !x.IsDeleted,
                cancellationToken);

        if (student is null)
            throw new KeyNotFoundException("Student not found.");

        // Duplicate Admission Number
        if (await _context.Students.AnyAsync(
            x => x.Id != request.Id &&
                 x.AdmissionNumber == request.AdmissionNumber,
            cancellationToken))
        {
            throw new InvalidOperationException("Admission number already exists.");
        }
        // Duplicate Email
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _context.Students.AnyAsync(
                x => x.Id != request.Id &&
                     x.Email == request.Email,
                cancellationToken))
            {
                throw new InvalidOperationException("Email already exists.");
            }
        }
            student.Update(
            request.AdmissionNumber,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.Email,
            request.PhoneNumber,
            request.Address,
            request.IsActive);
        await _context.SaveChangesAsync(cancellationToken);
    }
}