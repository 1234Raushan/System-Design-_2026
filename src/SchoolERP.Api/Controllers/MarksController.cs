using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Marks.Commands.CreateMark;
using SchoolERP.Application.Features.Marks.Commands.DeleteMark;
using SchoolERP.Application.Features.Marks.Commands.UpdateMark;
using SchoolERP.Application.Features.Marks.Queries.GetMarkById;
using SchoolERP.Application.Features.Marks.Queries.GetMarkList;

namespace SchoolERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MarksController : ControllerBase
{
    private readonly IMediator _mediator;


    public MarksController(IMediator mediator)
    {
        _mediator = mediator;
    }



    // CREATE

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateMarkCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(
            command,
            cancellationToken);


        return Ok(new
        {
            Success = true,
            MarkId = id
        });
    }



    // UPDATE

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateMarkCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            command,
            cancellationToken);


        return NoContent();
    }



    // DELETE

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {

        await _mediator.Send(
            new DeleteMarkCommand(id),
            cancellationToken);


        return NoContent();
    }




    // GET BY ID

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(
            new GetMarkByIdQuery(id),
            cancellationToken);


        return Ok(result);
    }




    // GET LIST

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] GetMarkListQuery query,
        CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(
            query,
            cancellationToken);


        return Ok(result);
    }
}