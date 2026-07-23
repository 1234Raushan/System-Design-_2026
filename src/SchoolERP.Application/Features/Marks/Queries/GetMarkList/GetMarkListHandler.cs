using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Marks.Queries.GetMarkById;

namespace SchoolERP.Application.Features.Marks.Queries.GetMarkList;

public sealed class GetMarkListHandler
    : IRequestHandler<GetMarkListQuery, PaginatedList<MarkListDto>>
{
    private readonly IApplicationDbContext _context;


    public GetMarkListHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<PaginatedList<MarkListDto>> Handle(
        GetMarkListQuery request,
        CancellationToken cancellationToken)
    {

        var pageNumber =
            request.PageNumber <= 0
            ? 1
            : request.PageNumber;


        var pageSize =
            request.PageSize <= 0
            ? 10
            : request.PageSize;



        var query = _context.Marks
            .AsNoTracking()
            .Where(x => !x.IsDeleted)
            .AsQueryable();



        // Search

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim();


            query = query.Where(x =>
                x.Exam.ExamName.Contains(search)
                ||
                (
                    x.StudentEnrollment.Student.FirstName
                    + " "
                    +
                    x.StudentEnrollment.Student.LastName
                )
                .Contains(search)
            );
        }



        var totalRecords =
            await query.CountAsync(cancellationToken);



        // Sorting

        query = request.SortBy?.ToLower() switch
        {

            "examname" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.Exam.ExamName)
                :
                query.OrderBy(x => x.Exam.ExamName),



            "studentname" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x =>
                    x.StudentEnrollment.Student.FirstName)
                :
                query.OrderBy(x =>
                    x.StudentEnrollment.Student.FirstName),



            "obtainedmarks" => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.ObtainedMarks)
                :
                query.OrderBy(x => x.ObtainedMarks),



            _ => request.SortDirection?.ToLower() == "desc"
                ?
                query.OrderByDescending(x => x.CreatedDate)
                :
                query.OrderBy(x => x.CreatedDate)

        };



        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new MarkListDto
            {
                Id = x.Id,

                ExamId = x.ExamId,

                ExamName = x.Exam.ExamName,


                StudentEnrollmentId =
                    x.StudentEnrollmentId,


                StudentName =
                    x.StudentEnrollment.Student.FirstName
                    + " " +
                    x.StudentEnrollment.Student.LastName,


                ObtainedMarks =
                    x.ObtainedMarks,


                MaximumMarks =
                    x.Exam.MaximumMarks

            })
            .ToListAsync(cancellationToken);



        return new PaginatedList<MarkListDto>(
            items,
            totalRecords,
            pageNumber,
            pageSize);
    }
}