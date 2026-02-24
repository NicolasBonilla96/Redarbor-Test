using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Redarbor.Application.Core.Abstractions.Database;
using System.Data;

namespace Redarbor.Persistence.Repositories.Dapper.Connection;

public class SqlConnectionFactory(
        IConfiguration configuration
    ) : ISqlConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}
