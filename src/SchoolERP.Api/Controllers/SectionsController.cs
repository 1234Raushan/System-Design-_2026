using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Sections.Commands.CreateSection;
using SchoolERP.Application.Features.Sections.Queries.GetSectionById;
using SchoolERP.Application.Features.Sections.Queries.GetSections;
using SchoolERP.Application.Features.Sections.Commands.UpdateSection;
using SchoolERP.Application.Features.Sections.Commands.DeleteSection;

namespace SchoolERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateSectionCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Section created successfully.",
            SectionId = id
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
    int id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetSectionByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] GetSectionsQuery query,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    UpdateSectionCommand command,
    CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Route Id and Request Id do not match.");

        await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Section updated successfully."
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
    int id,
    CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteSectionCommand(id),
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Section deleted successfully."
        });
    }
}