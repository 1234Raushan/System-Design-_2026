using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Exams.Commands.DeleteExam;

public sealed class DeleteExamHandler
    : IRequestHandler<DeleteExamCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExamHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteExamCommand request,
        CancellationToken cancellationToken)
    {
        var exam = await _context.Exams
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);

        if (exam is null)
        {
            throw new InvalidOperationException(
                "Exam not found.");
        }

        exam.SoftDelete();

        await _context.SaveChangesAsync(cancellationToken);
    }
}