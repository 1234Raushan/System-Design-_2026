using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.AcademicSessions.Commands.CreateAcademicSession;
using SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessionById;
using SchoolERP.Application.Features.AcademicSessions.Queries.GetAcademicSessions;
using SchoolERP.Application.Features.AcademicSessions.Commands.DeleteAcademicSession;

namespace SchoolERP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AcademicSessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AcademicSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAcademicSessionCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return Ok(new
        {
            Success = true,
            AcademicSessionId = id,
            Message = "Academic session created successfully."
        });
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAcademicSessionByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetAcademicSessionsQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteAcademicSessionCommand(id),
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Academic session deleted successfully."
        });
    }
}