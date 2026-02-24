using Dapper;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Roles.Dtos;

namespace Redarbor.Persistence.Repositories.Dapper.Queries;

public class RoleQueryRepository(
        ISqlConnectionFactory connectionFactory
    ) : IRoleQueryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task<RoleDto?> FindByIdAsync(Guid id)
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [Id]
                , [Name]
                , [Description]
            from [auth].[Roles]
            where [Id] = @Id
            """;

        return await _connection.QueryFirstOrDefaultAsync<RoleDto>(_query, new { Id = id });
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [Id]
                , [Name]
                , [Description]
            from [auth].[Roles]
            order by [Name]
            """;

        return await _connection.QueryAsync<RoleDto>(_query);
    }
}
