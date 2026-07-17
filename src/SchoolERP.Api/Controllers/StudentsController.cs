using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Common.Models;
using SchoolERP.Application.Features.Students.Commands.CreateStudent;
using SchoolERP.Application.Features.Students.Commands.DeleteStudent;
using SchoolERP.Application.Features.Students.Commands.UpdateStudent;
using SchoolERP.Application.Features.Students.Queries.GetStudentById;
using SchoolERP.Application.Features.Students.Queries.GetStudents;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateStudentCommand command,
            CancellationToken cancellationToken)
        {
            var studentId = await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Student created successfully.",
                StudentId = studentId
            });
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id,
         CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetStudentByIdQuery(id),
                cancellationToken);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery] GetStudentsQuery query,
        CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateStudentCommand command,CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Route Id and Request Id do not match.");

            await _mediator.Send(command, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Student updated successfully."
            });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteStudentCommand(id),
                cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Student deleted successfully."
            });
        }
    }
}
