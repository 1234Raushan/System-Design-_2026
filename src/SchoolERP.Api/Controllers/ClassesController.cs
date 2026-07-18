using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Classes.Commands.CreateClass;
using SchoolERP.Application.Features.Classes.Queries.GetClassById;
using SchoolERP.Application.Features.Classes.Queries.GetClasses;
using SchoolERP.Application.Features.Classes.Commands.UpdateClass;
using SchoolERP.Application.Features.Classes.Commands.DeleteClass;

namespace SchoolERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ClassesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateClassCommand command,
        CancellationToken cancellationToken)
    {
        var classId = await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Class created successfully.",
            ClassId = classId
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
    int id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetClassByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] GetClassesQuery query,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateClassCommand command,
    CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route Id and Request Id do not match.");

        await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Class updated successfully."
        });
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteClassCommand(id),
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Class deleted successfully."
        });
    }
}