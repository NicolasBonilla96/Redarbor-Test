using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Redarbor.Persistence.Contexts;
using System.Linq.Expressions;

namespace Redarbor.Persistence.Repositories;

public class Repository<TEntity, TKey>(
        RedarborDbContext db
    ) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
{
    protected readonly RedarborDbContext _db = db;

    private bool _isTrackingDisabled;

    public void Add(TEntity entity)
        => _db.Set<TEntity>().Add(entity);

    public void AddRange(List<TEntity> entities)
        => _db.Set<TEntity>().AddRange(entities);

    public void Delete(TEntity entity)
        => _db.Set<TEntity>().Remove(entity);

    public void DeleteRange(List<TEntity> entity)
        => _db.Set<TEntity>().RemoveRange(entity);

    public void Update(TEntity entity)
        => _db.Update(entity);

    public void Tracking(bool disableTracking)
        => _isTrackingDisabled = disableTracking;

    protected IQueryable<TEntity> FetchQueryable(Expression<Func<TEntity, bool>> predicate)
        => ConfigureQuery(predicate);

    public virtual async Task<TEntity?> FindByKeyAsync(TKey id, CancellationToken cancellationToken = default)
        => await _db.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<TEntity?> FindByintkeyAsync(Int32 id, CancellationToken cancellationToken = default)
        => await _db.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<TEntity?> FindBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await ConfigureQuery(predicate).FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = ConfigureQuery(null);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetBySpecAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).ToListAsync(cancellationToken);

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await ConfigureQuery(predicate).ToListAsync(cancellationToken);

    public async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).CountAsync(cancellationToken);

    public async Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).AnyAsync(cancellationToken);

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => await ConfigureQuery(predicate).AnyAsync(cancellationToken);

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        var evaluator = new SpecificationEvaluator();
        return evaluator.GetQuery(_db.Set<TEntity>().AsQueryable(), specification);
    }

    private IQueryable<TEntity> ConfigureQuery(Expression<Func<TEntity, bool>>? predicate)
    {
        var query = _db.Set<TEntity>().AsQueryable();
        if (_isTrackingDisabled) query = query.AsNoTracking();
        if (predicate != null) query = query.Where(predicate);
        return query;
    }
}