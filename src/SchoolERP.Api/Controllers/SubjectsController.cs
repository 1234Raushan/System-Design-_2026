using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Subjects.Commands.CreateSubject;
using SchoolERP.Application.Features.Subjects.Queries.GetSubjectById;
using SchoolERP.Application.Features.Subjects.Queries.GetSubjects;
using SchoolERP.Application.Features.Subjects.Commands.UpdateSubject;
using SchoolERP.Application.Features.Subjects.Commands.DeleteSubject;

namespace SchoolERP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateSubjectCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Subject created successfully.",
                SubjectId = id
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetSubjectByIdQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetSubjectsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateSubjectCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Subject updated successfully."
            });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteSubjectCommand(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Subject deleted successfully."
            });
        }
    }
}
