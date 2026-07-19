using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Teachers.Commands.UpdateTeacher;

public sealed class UpdateTeacherHandler
    : IRequestHandler<UpdateTeacherCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTeacherHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (teacher is null)
            throw new KeyNotFoundException("Teacher not found.");

        var duplicateEmail = await _context.Teachers
            .AnyAsync(
                x => x.Id != request.Id &&
                     x.Email == request.Email &&
                     !x.IsDeleted,
                cancellationToken);

        if (duplicateEmail)
            throw new InvalidOperationException("Email already exists.");

        teacher.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Gender,
            request.DateOfBirth,
            request.Qualification,
            request.Experience,
            request.Address,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}