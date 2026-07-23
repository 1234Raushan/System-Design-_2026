using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.StudentAttendance.Commands.CreateAttendance;
using SchoolERP.Application.Features.StudentAttendance.Commands.DeleteAttendance;
using SchoolERP.Application.Features.StudentAttendance.Commands.UpdateAttendance;
using SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceById;
using SchoolERP.Application.Features.StudentAttendance.Queries.GetAttendanceList;
namespace SchoolERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentAttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentAttendanceCommand command,CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(
                command,
                cancellationToken);


            return Ok(new
            {
                Success = true,
                AttendanceSessionId = id
            });
        }
        [HttpPut]
        public async Task<IActionResult> Update(
            UpdateStudentAttendanceCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{attendanceSessionId:int}")]
        public async Task<IActionResult> Delete(
            int attendanceSessionId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteStudentAttendanceCommand(attendanceSessionId),
                cancellationToken);

            return NoContent();
        }
        [HttpGet("{attendanceSessionId:int}")]
        public async Task<IActionResult> GetById(
            int attendanceSessionId,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetStudentAttendanceByIdQuery(attendanceSessionId),
                cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetStudentAttendanceListQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
