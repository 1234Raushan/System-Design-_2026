using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Exams.Queries.GetExamById;

public sealed class GetExamByIdHandler
    : IRequestHandler<GetExamByIdQuery, ExamDetailsDto>
{
    private readonly IApplicationDbContext _context;

    public GetExamByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExamDetailsDto> Handle(
        GetExamByIdQuery request,
        CancellationToken cancellationToken)
    {
        var exam = await _context.Exams
            .AsNoTracking()
            .Where(x =>
                x.Id == request.Id &&
                !x.IsDeleted)
            .Select(x => new ExamDetailsDto
            {
                Id = x.Id,
                TeachingAssignmentId = x.TeachingAssignmentId,

                TeacherName =
                    x.TeachingAssignment.Teacher.FirstName + " " +
                    x.TeachingAssignment.Teacher.LastName,

                SubjectName =
                    x.TeachingAssignment.Subject.SubjectName,

                ClassName =
                    x.TeachingAssignment.Class.ClassName,

                SectionName =
                    x.TeachingAssignment.Section.SectionName,

                ExamName = x.ExamName,
                ExamType = x.ExamType,
                ExamDate = x.ExamDate,
                MaximumMarks = x.MaximumMarks,
                PassingMarks = x.PassingMarks,
                Description = x.Description,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (exam is null)
        {
            throw new InvalidOperationException(
                "Exam not found.");
        }

        return exam;
    }
}