using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Students.Commands.CreateStudent;

public sealed class CreateStudentHandler
    : IRequestHandler<CreateStudentCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateStudentHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateStudentCommand request,
        CancellationToken cancellationToken)
    {
        // Duplicate Admission Number
        if (await _context.Students.AnyAsync(
            x => x.AdmissionNumber == request.AdmissionNumber,
            cancellationToken))
        {
            throw new InvalidOperationException(
                "Admission number already exists.");
        }

        // Duplicate Roll Number
        if (await _context.Students.AnyAsync(
            x => x.RollNumber == request.RollNumber,
            cancellationToken))
        {
            throw new InvalidOperationException(
                "Roll number already exists.");
        }

        // Duplicate Email (optional)
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            if (await _context.Students.AnyAsync(
                x => x.Email == request.Email,
                cancellationToken))
            {
                throw new InvalidOperationException(
                    "Email already exists.");
            }
        }

        var student = new Student(
            request.AdmissionNumber,
            request.RollNumber,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.AdmissionDate,
            request.Email,
            request.PhoneNumber,
            request.Address);

        student.AssignClass(request.ClassId);
        student.AssignSection(request.SectionId);

        _context.Students.Add(student);

        await _context.SaveChangesAsync(cancellationToken);

        return student.Id;
    }
}