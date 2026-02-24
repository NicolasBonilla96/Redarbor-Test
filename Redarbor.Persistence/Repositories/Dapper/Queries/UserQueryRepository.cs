using Dapper;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Users.Dtos;

namespace Redarbor.Persistence.Repositories.Dapper.Queries;

public class UserQueryRepository(
        ISqlConnectionFactory connectionFactory
    ) : IUserQueryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task<UserDto?> FindByIdAsync(Guid id)
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
            	[u].[Id]
                , [u].[UserName]
            	, [u].[Email]
            	, [u].[PhoneNumber]
                , [r].[Name] [Role]
            from [auth].[User] [u]
            left outer join [auth].[UserRoles] [ur]
            	on [u].[Id] = [ur].[UserId]
            left outer join [auth].[Roles] [r]
            	on [ur].[RoleId] = [r].[Id]
            where [u].[Id] = @Id
            """;

        return await _connection.QueryFirstOrDefaultAsync<UserDto>(_query, new { Id = id });
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [u].[Id]
                , [u].[UserName]
                , [u].[Email]
                , [u].[PhoneNumber]
                , [r].[Name] [Role]
            from [auth].[User] [u]
            left outer join [auth].[UserRoles] [ur]
                on [u].[Id] = [ur].[UserId]
            left outer join [auth].[Roles] [r]
                on [ur].[RoleId] = [r].[Id]
            order by [u].[UserName]
            """;

        return await _connection.QueryAsync<UserDto>(_query);
    }
}
