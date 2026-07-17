using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolERP.Application.Features.Users.Commands.CreateUser;
using SchoolERP.Application.Features.Users.Commands.DeleteUser;
using SchoolERP.Application.Features.Users.Commands.UpdateUser;
using SchoolERP.Application.Features.Users.Queries.GetUserById;
using SchoolERP.Application.Features.Users.Queries.GetUsers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolERP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUserCommand> _createUserValidator;
        private readonly IValidator<UpdateUserCommand> _updateUserValidator;
        private readonly IValidator<DeleteUserCommand> _deleteUserValidator;

        public UsersController(
            IMediator mediator,
            IValidator<CreateUserCommand> createUserValidator,
            IValidator<UpdateUserCommand> updateUserValidator,
            IValidator<DeleteUserCommand> deleteUserValidator)
        {
            _mediator = mediator;
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
            _deleteUserValidator = deleteUserValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _createUserValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var userId = await _mediator.Send(command, cancellationToken);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = userId },
                    userId);
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
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetUsersQuery { PageNumber = pageNumber, PageSize = pageSize, SearchTerm = searchTerm, SortBy = sortBy, SortDirection = sortDirection}, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetUserByIdQuery(id),
                cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation failed",
                    Detail = "Invalid user id.",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            var validationResult = await _updateUserValidator.ValidateAsync(command with { Id = id }, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var userId = await _mediator.Send(command with { Id = id }, cancellationToken);
                return Ok(userId);
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
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var validationResult = await _deleteUserValidator.ValidateAsync(new DeleteUserCommand(id), cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                return ValidationProblem(new ValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                var userId = await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
                return Ok(userId);
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
}
