using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Teachers.DTOs;

namespace SchoolERP.Application.Features.Teachers.Queries.GetTeacherById;

public sealed class GetTeacherByIdHandler
    : IRequestHandler<GetTeacherByIdQuery, TeacherDto>
{
    private readonly IApplicationDbContext _context;

    public GetTeacherByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto> Handle(
        GetTeacherByIdQuery request,
        CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     !x.IsDeleted,
                cancellationToken);

        if (teacher is null)
            throw new KeyNotFoundException("Teacher not found.");

        return new TeacherDto
        {
            Id = teacher.Id,
            UserId = teacher.UserId,
            EmployeeCode = teacher.EmployeeCode,
            FirstName = teacher.FirstName,
            LastName = teacher.LastName,
            Email = teacher.Email,
            PhoneNumber = teacher.PhoneNumber,
            Gender = teacher.Gender,
            DateOfBirth = teacher.DateOfBirth,
            JoiningDate = teacher.JoiningDate,
            Qualification = teacher.Qualification,
            Experience = teacher.Experience,
            Address = teacher.Address,
            IsActive = teacher.IsActive
        };
    }
}