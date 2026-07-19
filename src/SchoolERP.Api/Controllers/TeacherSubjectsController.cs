using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.TeacherSubjects.Commands.AssignSubject;

namespace SchoolERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TeacherSubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherSubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Assign(
            AssignSubjectCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                TeacherSubjectId = id,
                Message = "Subject assigned successfully."
            });
        }
    }
}
