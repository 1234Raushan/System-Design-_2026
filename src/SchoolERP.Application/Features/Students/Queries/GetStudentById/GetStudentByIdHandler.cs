using MediatR;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Students.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Queries.GetStudentById
{
    public sealed class GetStudentByIdHandler
        : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDto> Handle(
            GetStudentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id && !x.IsDeleted,
                    cancellationToken);

            if (student is null)
                throw new KeyNotFoundException("Student not found.");

            return new StudentDto
            {
                Id = student.Id,
                AdmissionNumber = student.AdmissionNumber,
                RollNumber = student.RollNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                AdmissionDate = student.AdmissionDate,
                ClassId = student.ClassId,
                SectionId = student.SectionId,
                IsActive = student.IsActive
            };
        }
    }
}
