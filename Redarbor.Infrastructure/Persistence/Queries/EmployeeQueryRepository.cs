using Dapper;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Application.Core.Abstractions.Interfaces;
using Redarbor.Application.Features.Employees.Dtos;

namespace Redarbor.Infrastructure.Persistence.Queries;

public class EmployeeQueryRepository(
        ISqlConnectionFactory connectionFactory
    ) : IEmployeeQueryRepository
{
    private readonly ISqlConnectionFactory _connectionFactory = connectionFactory;

    public async Task<EmployeeDto?> FindByIdAsync(Guid id)
    {
        using var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
            	[e].[Id] [EmployeeId]
            	, [e].[CompanyId]
            	, [c].[Name] [Company]
            	, [e].[PortalId]
            	, [p].[Name] [Portal]
            	, [e].[StateId]
            	, [s].[Name] [State]
            	, [e].[Name]
            	, [u].[UserName]
            	, [u].[Email]
            	, [u].[PhoneNumber] [Phone]
            	, [e].[Fax]
            	, null [LastLogin]
            	, [ur].[RoleId]
            	, [r].[Name] [Role]
            	, [e].[CreatedOn]
            	, [uc].[UserName] [CreatedBy]
            	, [e].[UpdatedOn]
            	, [uu].[UserName] [UpdatedBy]
            	, [e].[DeletedOn] 
            	, [ud].[UserName] [DeletedBy]
            from [info].[Employees] [e]
            left outer join [auth].[User] [u]
            	on [e].[UserId] = [u].[Id]
            left outer join [type].[Companies] [c]
            	on [e].[CompanyId] = [c].[Id]
            left outer join [type].[Portals] [p]
            	on [e].[PortalId] = [p].[Id]
            left outer join [type].[States] [s]
            	on [e].[StateId] = [s].[Id]
            left outer join [auth].[User] [uc]
            	on [e].[CreatedBy] = [uc].[Id]
            left outer join [auth].[User] [uu]
            	on [e].[UpdatedBy] = [uu].[Id]
            left outer join [auth].[User] [ud]
            	on [e].[DeletedBy] = [ud].[Id]
            left outer join [auth].[UserRoles] [ur]
            	on [e].[UserId] = [ur].[UserId]
            left outer join [auth].[Roles] [r]
            	on [ur].[RoleId] = [r].[Id]
            where [e].[Id] = @Id
            """;

        return await _connection.QueryFirstOrDefaultAsync<EmployeeDto>(_query, new { Id = id });
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        using var _connection = _connectionFactory.CreateConnection();

        var _query = """
            select
            	[e].[Id] [EmployeeId]
            	, [e].[CompanyId]
            	, [c].[Name] [Company]
            	, [e].[PortalId]
            	, [p].[Name] [Portal]
            	, [e].[StateId]
            	, [s].[Name] [State]
            	, [e].[Name]
            	, [u].[UserName]
            	, [u].[Email]
            	, [u].[PhoneNumber] [Phone]
            	, [e].[Fax]
            	, null [LastLogin]
            	, [ur].[RoleId]
            	, [r].[Name] [Role]
            	, [e].[CreatedOn]
            	, [uc].[UserName] [CreatedBy]
            	, [e].[UpdatedOn]
            	, [uu].[UserName] [UpdatedBy]
            	, [e].[DeletedOn] 
            	, [ud].[UserName] [DeletedBy]
            from [info].[Employees] [e]
            left outer join [auth].[User] [u]
            	on [e].[UserId] = [u].[Id]
            left outer join [type].[Companies] [c]
            	on [e].[CompanyId] = [c].[Id]
            left outer join [type].[Portals] [p]
            	on [e].[PortalId] = [p].[Id]
            left outer join [type].[States] [s]
            	on [e].[StateId] = [s].[Id]
            left outer join [auth].[User] [uc]
            	on [e].[CreatedBy] = [uc].[Id]
            left outer join [auth].[User] [uu]
            	on [e].[UpdatedBy] = [uu].[Id]
            left outer join [auth].[User] [ud]
            	on [e].[DeletedBy] = [ud].[Id]
            left outer join [auth].[UserRoles] [ur]
            	on [e].[UserId] = [ur].[UserId]
            left outer join [auth].[Roles] [r]
            	on [ur].[RoleId] = [r].[Id]
            order by [e].[Name]
            """;

        return await _connection.QueryAsync<EmployeeDto>(_query);
    }
}
