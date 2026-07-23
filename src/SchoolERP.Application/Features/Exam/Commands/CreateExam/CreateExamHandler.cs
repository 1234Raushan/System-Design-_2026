using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Exams.Commands.CreateExam;

public sealed class CreateExamHandler
    : IRequestHandler<CreateExamCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateExamHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateExamCommand request,
        CancellationToken cancellationToken)
    {
        var teachingAssignmentExists =
            await _context.TeachingAssignments
            .AnyAsync(x =>
                x.Id == request.TeachingAssignmentId &&
                !x.IsDeleted,
                cancellationToken);

        if (!teachingAssignmentExists)
            throw new InvalidOperationException(
                "Teaching Assignment not found.");

        var exam = new Exam(
            request.TeachingAssignmentId,
            request.ExamName,
            request.ExamType,
            request.ExamDate,
            request.MaximumMarks,
            request.PassingMarks,
            request.Description);

        _context.Exams.Add(exam);

        await _context.SaveChangesAsync(cancellationToken);

        return exam.Id;
    }
}