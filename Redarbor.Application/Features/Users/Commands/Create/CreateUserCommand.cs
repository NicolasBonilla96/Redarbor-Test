using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Users.Dtos;

namespace Redarbor.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(
        CreateUserRequest User
    ) : IRequest<Result<Guid>>;