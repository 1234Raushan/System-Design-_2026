using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.TeachingAssignments.Commands.CreateTeachingAssignment;
using SchoolERP.Application.Features.TeachingAssignments.Commands.DeleteTeachingAssignment;
using SchoolERP.Application.Features.TeachingAssignments.Commands.UpdateTeachingAssignment;
using SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentById;
using SchoolERP.Application.Features.TeachingAssignments.Queries.GetTeachingAssignmentList;

namespace SchoolERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeachingAssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeachingAssignmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTeachingAssignmentCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Teaching Assignment created successfully.",
            TeachingAssignmentId = id
        });
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetTeachingAssignmentByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetTeachingAssignmentListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            query,
            cancellationToken);

        return Ok(result);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateTeachingAssignmentCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest(
                "Route Id and Request Id do not match.");


        await _mediator.Send(
            command,
            cancellationToken);


        return Ok(new
        {
            Success = true,
            Message = "Teaching Assignment updated successfully."
        });
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteTeachingAssignmentCommand(id),
            cancellationToken);


        return Ok(new
        {
            Success = true,
            Message = "Teaching Assignment deleted successfully."
        });
    }
}