using System.Data;

namespace Redarbor.Application.Core.Abstractions.Database;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
