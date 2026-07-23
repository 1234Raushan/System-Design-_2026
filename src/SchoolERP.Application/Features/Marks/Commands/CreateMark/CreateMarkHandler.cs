using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Marks.Commands.CreateMark;

public sealed class CreateMarkHandler
    : IRequestHandler<CreateMarkCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMarkHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<int> Handle(
        CreateMarkCommand request,
        CancellationToken cancellationToken)
    {

        // 1. Check Exam Exists

        var exam = await _context.Exams
            .FirstOrDefaultAsync(x =>
                x.Id == request.ExamId &&
                !x.IsDeleted,
                cancellationToken);


        if (exam is null)
        {
            throw new InvalidOperationException(
                "Exam not found.");
        }



        // 2. Validate Maximum Marks

        foreach (var item in request.Students)
        {
            if (item.ObtainedMarks > exam.MaximumMarks)
            {
                throw new InvalidOperationException(
                    $"Obtained marks cannot be greater than maximum marks ({exam.MaximumMarks}).");
            }
        }



        // 3. Get Student Enrollments

        var studentIds = request.Students
            .Select(x => x.StudentEnrollmentId)
            .ToList();



        var enrollments = await _context.StudentEnrollments
            .Where(x =>
                studentIds.Contains(x.Id)
                &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);



        if (enrollments.Count != studentIds.Count)
        {
            throw new InvalidOperationException(
                "One or more student enrollment not found.");
        }




        // 4. Duplicate Marks Check

        var existingMarks = await _context.Marks
            .Where(x =>
                x.ExamId == request.ExamId
                &&
                studentIds.Contains(x.StudentEnrollmentId)
                &&
                !x.IsDeleted)
            .AnyAsync(cancellationToken);


        if (existingMarks)
        {
            throw new InvalidOperationException(
                "Marks already entered for one or more students.");
        }



        // 5. Create Marks

        var marks = request.Students
            .Select(x =>
                new Mark(
                    request.ExamId,
                    x.StudentEnrollmentId,
                    x.ObtainedMarks,
                    x.Remarks))
            .ToList();



        _context.Marks.AddRange(marks);


        await _context.SaveChangesAsync(
            cancellationToken);



        return marks.First().Id;
    }
}