using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using SchoolERP.Domain.Entities;

namespace SchoolERP.Application.Features.Classes.Commands.CreateClass;

public sealed class CreateClassHandler
    : IRequestHandler<CreateClassCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateClassHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateClassCommand request,
        CancellationToken cancellationToken)
    {
        if (await _context.classes.AnyAsync(
            x => x.ClassName == request.ClassName,
            cancellationToken))
        {
            throw new InvalidOperationException("Class name already exists.");
        }

        if (await _context.classes.AnyAsync(
            x => x.ClassCode == request.ClassCode,
            cancellationToken))
        {
            throw new InvalidOperationException("Class code already exists.");
        }

        var entity = new Class_A(
            request.ClassName,
            request.ClassCode,
            request.Description, request.isActive);

        _context.classes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}