using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Application.Features.Timetables.DTOs;

namespace SchoolERP.Application.Features.Timetables.Queries.GetTimetableById;

public sealed class GetTimetableByIdHandler
    : IRequestHandler<GetTimetableByIdQuery, TimetableDto?>
{
    private readonly IApplicationDbContext _context;

    public GetTimetableByIdHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<TimetableDto?> Handle(
        GetTimetableByIdQuery request,
        CancellationToken cancellationToken)
    {
        var timetable = await _context.Timetables
            .AsNoTracking()

            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Teacher)

            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Subject)

            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Class)

            .Include(x => x.TeachingAssignment)
                .ThenInclude(x => x.Section)

            .Where(x =>
                x.Id == request.Id &&
                !x.IsDeleted)

            .Select(x => new TimetableDto
            {
                Id = x.Id,

                TeachingAssignmentId =
                    x.TeachingAssignmentId,


                TeacherId =
                    x.TeachingAssignment.TeacherId,

                TeacherName =
                    x.TeachingAssignment.Teacher.FirstName
                    + " "
                    + x.TeachingAssignment.Teacher.LastName,


                SubjectId =
                    x.TeachingAssignment.SubjectId,

                SubjectName =
                    x.TeachingAssignment.Subject.SubjectName,


                ClassId =
                    x.TeachingAssignment.ClassId,

                ClassName =
                    x.TeachingAssignment.Class.ClassName,


                SectionId =
                    x.TeachingAssignment.SectionId,

                SectionName =
                    x.TeachingAssignment.Section.SectionName,


                DayOfWeek = x.DayOfWeek,

                PeriodNumber = x.PeriodNumber,

                StartTime = x.StartTime,

                EndTime = x.EndTime,

                RoomNumber = x.RoomNumber,

                Remarks = x.Remarks,

                IsActive = x.IsActive
            })

            .FirstOrDefaultAsync(cancellationToken);


        return timetable;
    }
}