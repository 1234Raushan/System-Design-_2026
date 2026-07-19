using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.AcademicSessions.Commands.CreateAcademicSession;

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
}