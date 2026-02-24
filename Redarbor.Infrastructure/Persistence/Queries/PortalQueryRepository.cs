using Dapper;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Infrastructure.Persistence.Queries;

public class PortalQueryRepository(
        ISqlConnectionFactory connectionFactory
    ) : IPortalQueryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task<MasterDto?> FindByIdAsync(int id)
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [Id]
                , [Name]
            from [type].[Portals]
            where [Id] = @Id
            """;

        return await _connection.QueryFirstOrDefaultAsync<MasterDto>(_query, new { Id = id });
    }

    public async Task<IEnumerable<MasterDto>> GetAllAsync()
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [Id]
                , [Name]
            from [type].[Portals]
            """;

        return await _connection.QueryAsync<MasterDto>(_query);
    }
}
