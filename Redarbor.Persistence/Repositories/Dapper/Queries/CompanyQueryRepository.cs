using Dapper;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Master.Dtos;

namespace Redarbor.Persistence.Repositories.Dapper.Queries;

public class CompanyQueryRepository(
        ISqlConnectionFactory connectionFactory
    ) : ICompanyQueryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task<MasterDto?> FindByIdAsync(int id)
    {
        var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
                [Id]
                , [Name]
            from [type].[Companies]
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
            from [type].[Companies]
            """;

        return await _connection.QueryAsync<MasterDto>(_query);
    }
}
