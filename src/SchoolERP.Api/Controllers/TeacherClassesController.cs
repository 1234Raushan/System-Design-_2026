using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.TeacherClasses.Commands.AssignClass;
using SchoolERP.Application.Features.TeacherClasses.Queries.GetTeacherClasses;
using SchoolERP.Application.Features.TeacherClasses.Commands.RemoveClass;

namespace SchoolERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TeacherClassesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Assign(
            AssignClassCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                TeacherClassId = id,
                Message = "Class assigned successfully."
            });
        }
        [HttpGet("{teacherId:int}")]
        public async Task<IActionResult> GetTeacherClasses(
            int teacherId,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetTeacherClassesQuery(teacherId),
                cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(
           int id,
           CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new RemoveClassCommand(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Class removed successfully."
            });
        }
    }
}
