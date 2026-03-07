using System.Linq.Expressions;
using App.Interfaces.Common;
using Core.Models.Common;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Common
{
  public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
  {
    protected readonly BackendContext _context;

    public RepositoryBase(BackendContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<TEntity>> ToListAsync(
      Expression<Func<TEntity, bool>>? predicate = null,
      List<Expression<Func<TEntity, object>>>? includes = null, Func<IQueryable<TEntity>,
      IOrderedQueryable<TEntity>>? orderBy = null, bool disableTracking = true,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<TEntity> query = _context.Set<TEntity>();
      if (disableTracking) query = query.AsNoTracking();
      if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
      if (predicate != null) query = query.Where(predicate);
      if (orderBy != null) return await orderBy(query).ToListAsync(cancellationToken);
      return await query.OrderByDescending(x => x.DateCreate).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetFirstAsync(
      Expression<Func<TEntity, bool>>? predicate = null,
      List<Expression<Func<TEntity, object>>>? includes = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<TEntity> query = _context.Set<TEntity>();
      if (disableTracking) query = query.AsNoTracking();
      if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
      if (predicate != null) query = query.Where(predicate);
      return await query.OrderByDescending(x => x.DateCreate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> AddAsync(
      TEntity entity,
      CancellationToken cancellationToken = default
    )
    {
      ArgumentNullException.ThrowIfNull(entity);
      _context.Entry(entity).State = EntityState.Added;
      await _context.SaveChangesAsync(cancellationToken);
      return entity;
    }

    public async Task AddRangeAsync(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default
    )
    {
      await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
      await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
      TEntity entity,
      CancellationToken cancellationToken = default
    )
    {
      ArgumentNullException.ThrowIfNull(entity);
      _context.Entry(entity).State = EntityState.Modified;
      await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default
    )
    {
      _context.Set<TEntity>().UpdateRange(entities);
      await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
      TEntity entity,
      CancellationToken cancellationToken = default
    )
    {
      ArgumentNullException.ThrowIfNull(entity);
      _context.Entry(entity).State = EntityState.Deleted;
      await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default
    )
    {
      _context.Set<TEntity>().RemoveRange(entities);
      await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
      Expression<Func<TEntity, bool>>? predicate = null,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<TEntity> query = _context.Set<TEntity>();
      if (predicate != null) query = query.Where(predicate);
      return await query.CountAsync(cancellationToken);
    }

    public TEntity Detach(TEntity entity)
    {
      _context.Entry(entity).State = EntityState.Detached;
      return entity;
    }
  }
}