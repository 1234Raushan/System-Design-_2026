using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.StudentEnrollments.DTOs;

namespace SchoolERP.Application.Features.StudentEnrollments.Queries.GetEnrollmentById;

public sealed class GetEnrollmentByIdHandler
    : IRequestHandler<GetEnrollmentByIdQuery, EnrollmentDto>
{
    private readonly IApplicationDbContext _context;

    public GetEnrollmentByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EnrollmentDto> Handle(
        GetEnrollmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var enrollment = await _context.StudentEnrollments
            .AsNoTracking()
            .Where(x => x.Id == request.Id && !x.IsDeleted)
            .Select(x => new EnrollmentDto
            {
                Id = x.Id,

                StudentId = x.StudentId,
                StudentName = x.Student.FirstName + " " + x.Student.LastName,

                AcademicSessionId = x.AcademicSessionId,
                AcademicSession = x.AcademicSession.SessionName,

                ClassId = x.ClassId,
                ClassName = x.Class.ClassName,

                SectionId = x.SectionId,
                SectionName = x.Section.SectionName,

                RollNumber = x.RollNumber,
                AdmissionDate = x.AdmissionDate,

                Status = x.Status.ToString(),

                Remarks = x.Remarks,

                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (enrollment is null)
            throw new KeyNotFoundException("Enrollment not found.");

        return enrollment;
    }
}