using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Timetables.Commands.CreateTimetable;
using SchoolERP.Application.Features.Timetables.Commands.DeleteTimetable;
using SchoolERP.Application.Features.Timetables.Commands.UpdateTimetable;
using SchoolERP.Application.Features.Timetables.Queries.GetTimetableById;
using SchoolERP.Application.Features.Timetables.Queries.GetTimetableList;

namespace SchoolERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetablesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimetablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateTimetableCommand command,
            CancellationToken cancellationToken)
        {
            var timetableId = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Timetable created successfully.",
                TimetableId = timetableId
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
           int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetTimetableByIdQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetTimetableListQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTimetableCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Timetable updated successfully."
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteTimetableCommand(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Timetable deleted successfully."
            });
        }
    }
}