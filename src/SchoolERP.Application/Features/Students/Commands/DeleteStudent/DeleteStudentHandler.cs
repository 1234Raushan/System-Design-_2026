using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolERP.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Features.Students.Commands.DeleteStudent
{
    public sealed class DeleteStudentHandler
        : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStudentHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteStudentCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(
                    x => x.Id == request.Id && !x.IsDeleted,
                    cancellationToken);

            if (student is null)
                throw new KeyNotFoundException("Student not found.");

            student.SoftDelete();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
