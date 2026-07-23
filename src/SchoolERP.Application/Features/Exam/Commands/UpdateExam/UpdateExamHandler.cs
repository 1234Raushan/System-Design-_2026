using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Exams.Commands.UpdateExam;

public sealed class UpdateExamHandler
    : IRequestHandler<UpdateExamCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateExamHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateExamCommand request,
        CancellationToken cancellationToken)
    {
        var exam = await _context.Exams
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (exam is null)
            throw new InvalidOperationException("Exam not found.");

        exam.Update(
            request.ExamName,
            request.ExamType,
            request.ExamDate,
            request.MaximumMarks,
            request.PassingMarks,
            request.Description,
            request.IsActive);

        await _context.SaveChangesAsync(cancellationToken);
    }
}