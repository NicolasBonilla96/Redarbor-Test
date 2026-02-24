using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Features.Users.Dtos;

namespace Redarbor.Application.Features.Users.Queries.GetAll;

public sealed record GetAllUserQuery : IRequest<Result<IEnumerable<UserDto>>>;