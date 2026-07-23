using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Marks.Queries.GetMarkById;

public sealed class GetMarkByIdHandler
    : IRequestHandler<GetMarkByIdQuery, MarkListDto>
{
    private readonly IApplicationDbContext _context;


    public GetMarkByIdHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<MarkListDto> Handle(
        GetMarkByIdQuery request,
        CancellationToken cancellationToken)
    {

        var mark = await _context.Marks
            .AsNoTracking()
            .Where(x =>
                x.Id == request.Id &&
                !x.IsDeleted)
            .Select(x => new MarkListDto
            {
                Id = x.Id,


                ExamId = x.ExamId,

                ExamName = x.Exam.ExamName,


                StudentEnrollmentId =
                    x.StudentEnrollmentId,


                StudentId =
                    x.StudentEnrollment.StudentId,


                StudentName =
                    x.StudentEnrollment.Student.FirstName
                    + " " +
                    x.StudentEnrollment.Student.LastName,


                ObtainedMarks =
                    x.ObtainedMarks,


                MaximumMarks =
                    x.Exam.MaximumMarks,


                PassingMarks =
                    x.Exam.PassingMarks,


                Remarks =
                    x.Remarks

            })
            .FirstOrDefaultAsync(cancellationToken);



        if (mark is null)
        {
            throw new InvalidOperationException(
                "Mark record not found.");
        }



        return mark;
    }
}