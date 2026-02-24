using MediatR;
using Redarbor.Application.Common.Results;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Users.Dtos;
using System.Net;

namespace Redarbor.Application.Features.Users.Queries.GetById;

public sealed class GetUserQueryHandler(
        IUserQueryRepository query
    ) : IRequestHandler<GetUserQuery, Result<UserDto>>
{
    private readonly IUserQueryRepository _query = query;

    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _query.FindByIdAsync(request.UserId);
        return user is not null
                ? Result<UserDto>.Success(user)
                : Result<UserDto>.Failure($"No se encontró ningún usuario bajo el id '{request.UserId}'", (int)HttpStatusCode.NotFound);
    }            
}
