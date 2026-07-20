using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.StudentEnrollments.Commands.CreateEnrollment;
using SchoolERP.Application.Features.StudentEnrollments.Commands.UpdateEnrollment;

namespace SchoolERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class StudentEnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentEnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEnrollmentCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Student enrolled successfully.",
            Data = id
        });
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id,
    UpdateEnrollmentCommand command,
    CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Enrollment updated successfully."
        });
    }
}