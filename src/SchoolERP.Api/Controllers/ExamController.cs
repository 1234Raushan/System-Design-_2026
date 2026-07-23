using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Exams.Commands.CreateExam;
using SchoolERP.Application.Features.Exams.Commands.DeleteExam;
using SchoolERP.Application.Features.Exams.Commands.UpdateExam;
using SchoolERP.Application.Features.Exams.Queries.GetExamById;
using SchoolERP.Application.Features.Exams.Queries.GetExamList;

namespace SchoolERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ExamController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateExamCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            ExamId = id
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateExamCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            command,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteExamCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetExamByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] GetExamListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            query,
            cancellationToken);

        return Ok(result);
    }
}