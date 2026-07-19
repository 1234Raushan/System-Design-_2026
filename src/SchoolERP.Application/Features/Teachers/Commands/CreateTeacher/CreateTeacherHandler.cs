using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Teachers.Commands.CreateTeacher;

public sealed class CreateTeacherHandler
    : IRequestHandler<CreateTeacherCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTeacherHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        // 1. User should exist
        var userExists = await _context.Users
            .AnyAsync(x => x.Id == request.UserId,
                cancellationToken);

        if (!userExists)
            throw new InvalidOperationException("User not found.");

        // 2. User should not already be assigned as Teacher
        var teacherExists = await _context.Teachers
            .AnyAsync(x => x.UserId == request.UserId &&
                           !x.IsDeleted,
                cancellationToken);

        if (teacherExists)
            throw new InvalidOperationException("Teacher already exists for this user.");

        // 3. EmployeeCode must be unique
        var employeeCodeExists = await _context.Teachers
            .AnyAsync(x => x.EmployeeCode == request.EmployeeCode &&
                           !x.IsDeleted,
                cancellationToken);

        if (employeeCodeExists)
            throw new InvalidOperationException("Employee Code already exists.");

        // 4. Email must be unique
        var emailExists = await _context.Teachers
            .AnyAsync(x => x.Email == request.Email &&
                           !x.IsDeleted,
                cancellationToken);

        if (emailExists)
            throw new InvalidOperationException("Email already exists.");

        var teacher = new Teacher(
            request.UserId,
            request.EmployeeCode,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Gender,
            request.DateOfBirth,
            request.JoiningDate,
            request.Qualification,
            request.Experience,
            request.Address);

        _context.Teachers.Add(teacher);

        await _context.SaveChangesAsync(cancellationToken);

        return teacher.Id;
    }
}