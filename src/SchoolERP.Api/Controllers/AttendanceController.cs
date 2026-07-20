using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Attendance.Commands.CreateAttendance;

namespace SchoolERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAttendanceCommand command,CancellationToken cancellationToken)
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
    }
}
