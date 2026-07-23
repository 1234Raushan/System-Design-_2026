using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;

namespace SchoolERP.Application.Features.Marks.Commands.DeleteMark;

public sealed class DeleteMarkHandler
    : IRequestHandler<DeleteMarkCommand>
{
    private readonly IApplicationDbContext _context;


    public DeleteMarkHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }



    public async Task Handle(
        DeleteMarkCommand request,
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



        mark.SoftDelete();



        await _context.SaveChangesAsync(
            cancellationToken);
    }
}