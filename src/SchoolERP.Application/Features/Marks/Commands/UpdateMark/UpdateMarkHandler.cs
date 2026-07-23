using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Marks.Commands.UpdateMark;

public sealed class UpdateMarkHandler
    : IRequestHandler<UpdateMarkCommand>
{
    private readonly IApplicationDbContext _context;


    public UpdateMarkHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task Handle(
        UpdateMarkCommand request,
        CancellationToken cancellationToken)
    {

        var mark = await _context.Marks
            .FirstOrDefaultAsync(x =>
                x.Id == request.Id &&
                !x.IsDeleted,
                cancellationToken);


        if (mark is null)
        {
            throw new InvalidOperationException(
                "Mark record not found.");
        }



        var exam = await _context.Exams
            .FirstOrDefaultAsync(x =>
                x.Id == mark.ExamId &&
                !x.IsDeleted,
                cancellationToken);



        if (exam is null)
        {
            throw new InvalidOperationException(
                "Exam not found.");
        }



        if (request.ObtainedMarks > exam.MaximumMarks)
        {
            throw new InvalidOperationException(
                $"Marks cannot be greater than maximum marks ({exam.MaximumMarks}).");
        }



        mark.Update(
            request.ObtainedMarks,
            request.Remarks,
            mark.IsActive);



        await _context.SaveChangesAsync(
            cancellationToken);
    }
}