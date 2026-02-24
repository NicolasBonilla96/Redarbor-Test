using System.Data.Common;

namespace Redarbor.Application.Core.Abstractions.Database;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public Task<DbTransaction> BeginTransactionAsync();
}
