using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.StudentEnrollments.DTOs;

namespace SchoolERP.Application.Features.StudentEnrollments.Queries.GetEnrollments;

public sealed class GetEnrollmentsHandler
    : IRequestHandler<GetEnrollmentsQuery, IReadOnlyList<EnrollmentDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEnrollmentsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<EnrollmentDto>> Handle(
        GetEnrollmentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.StudentEnrollments
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (request.AcademicSessionId.HasValue)
        {
            query = query.Where(x =>
                x.AcademicSessionId == request.AcademicSessionId.Value);
        }

        if (request.ClassId.HasValue)
        {
            query = query.Where(x =>
                x.ClassId == request.ClassId.Value);
        }

        if (request.SectionId.HasValue)
        {
            query = query.Where(x =>
                x.SectionId == request.SectionId.Value);
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(x =>
                x.IsActive == request.IsActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();

            query = query.Where(x =>
                x.Student.FirstName.Contains(search) ||
                x.Student.LastName.Contains(search) ||
                x.RollNumber.ToString().Contains(search));
        }

        return await query
            .OrderBy(x => x.RollNumber)
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
            .ToListAsync(cancellationToken);
    }
}