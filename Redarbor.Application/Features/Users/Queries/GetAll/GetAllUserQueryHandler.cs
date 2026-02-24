using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Users.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Users.Queries.GetAll;

public sealed class GetAllUserQueryHandler(
        IUserQueryRepository query
    ) : IRequestHandler<GetAllUserQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUserQueryRepository _query = query;

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _query.GetAllAsync();
        return users.Any()
            ? Result<IEnumerable<UserDto>>.Success(users)
            : Result<IEnumerable<UserDto>>.Failure("No se encontró información de usuarios.", (int)HttpStatusCode.NotFound);
    }
}
