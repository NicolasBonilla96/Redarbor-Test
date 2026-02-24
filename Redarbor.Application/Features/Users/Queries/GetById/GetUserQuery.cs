using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Users.Dtos;

namespace Redarbor.Application.Features.Users.Queries.GetById;

public sealed record GetUserQuery(
        Guid UserId
    ) : IRequest<Result<UserDto>>;