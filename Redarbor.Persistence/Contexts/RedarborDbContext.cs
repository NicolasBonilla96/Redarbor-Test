using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Redarbor.Application.Core.Abstractions.Database;
using Redarbor.Domain.Abstractions;
using Redarbor.Domain.Entities.Auth;
using Redarbor.Persistence.Extensions;
using System.Data.Common;
using System.Linq.Expressions;

namespace Redarbor.Persistence.Contexts;

public class RedarborDbContext(
        DbContextOptions<RedarborDbContext> options
    ) : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole, UserLogin,
        IdentityRoleClaim<Guid>, UserToken>(options),
        IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        foreach(var entityType in builder.Model.GetEntityTypes())
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                builder.Entity(entityType.ClrType).HasQueryFilter(DeletedFilter(entityType.ClrType));
    }

    private static LambdaExpression DeletedFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "e");
        var property = Expression.Property(parameter, nameof(AuditableEntity.IsDeleted));
        var condition = Expression.Equal(property, Expression.Constant(false));
        return Expression.Lambda(condition, parameter);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        => await base.SaveChangesAsync(cancellationToken);

    public async Task<DbTransaction> BeginTransactionAsync()
    {
        var transaction = await Database.BeginTransactionAsync();
        return transaction.GetDbTransaction();
    }
}
