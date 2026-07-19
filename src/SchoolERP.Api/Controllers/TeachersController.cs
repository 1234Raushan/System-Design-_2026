using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Teachers.Commands.CreateTeacher;
using SchoolERP.Application.Features.Teachers.Queries.GetTeacherById;
using SchoolERP.Application.Features.Teachers.Queries.GetTeachers;
using SchoolERP.Application.Features.Teachers.Commands.UpdateTeacher;
using SchoolERP.Application.Features.Teachers.Commands.DeleteTeacher;

namespace SchoolERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateTeacherCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                TeacherId = id,
                Message = "Teacher created successfully."
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetTeacherByIdQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetTeachersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTeacherCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Teacher updated successfully."
            });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteTeacherCommand(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Teacher deleted successfully."
            });
        }

    }
}
