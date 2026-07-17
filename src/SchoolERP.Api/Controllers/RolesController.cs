using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Roles.Commands.CreateRole;
using SchoolERP.Application.Features.Roles.Commands.DeleteRole;
using SchoolERP.Application.Features.Roles.Commands.UpdateRole;
using SchoolERP.Application.Features.Roles.DTOs;
using SchoolERP.Application.Features.Roles.Queries.GetRoleById;
using SchoolERP.Application.Features.Roles.Queries.GetRoles;

namespace SchoolERP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public sealed class RolesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateRoleCommand> _createRoleValidator;
    private readonly IValidator<UpdateRoleCommand> _updateRoleValidator;
    private readonly IValidator<DeleteRoleCommand> _deleteRoleValidator;

    public RolesController(
        IMediator mediator,
        IMapper mapper,
        IValidator<CreateRoleCommand> createRoleValidator,
        IValidator<UpdateRoleCommand> updateRoleValidator,
        IValidator<DeleteRoleCommand> deleteRoleValidator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _createRoleValidator = createRoleValidator;
        _updateRoleValidator = updateRoleValidator;
        _deleteRoleValidator = deleteRoleValidator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateRoleCommand>(request);
        var validationResult = await _createRoleValidator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        try
        {
            var roleId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = roleId }, roleId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortDirection = null,
        [FromQuery] bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRolesQuery 
        { PageNumber = pageNumber, PageSize = pageSize, SearchTerm = searchTerm, SortBy = sortBy, SortDirection = sortDirection, IsActive = isActive }, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery(id), cancellationToken);
        return role is null ? NotFound() : Ok(role);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateRoleCommand>(request) with { Id = id };
        var validationResult = await _updateRoleValidator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        try
        {
            var roleId = await _mediator.Send(command, cancellationToken);
            return Ok(roleId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var validationResult = await _deleteRoleValidator.ValidateAsync(new DeleteRoleCommand(id), cancellationToken);

        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ErrorMessage).ToArray()))
            {
                Status = StatusCodes.Status400BadRequest
            });
        }

        try
        {
            var roleId = await _mediator.Send(new DeleteRoleCommand(id), cancellationToken);
            return Ok(roleId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }
}
