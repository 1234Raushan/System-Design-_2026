using MediatR;

namespace SchoolERP.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(int Id) : IRequest<int>;
