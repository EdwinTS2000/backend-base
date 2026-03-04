using System.Linq.Expressions;
using Core.Models.Common;

namespace App.Interfaces.Common
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        // Task<IDbContextTransaction> BeginTransactionAsync();

        Task<IReadOnlyList<TEntity>> ToListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            List<Expression<Func<TEntity, object>>>? includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        );
        Task AddRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default
        );

        Task UpdateAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task UpdateRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default
        );

        Task RemoveAsync(
            TEntity entity,
            CancellationToken cancellationToken = default
        );

        Task RemoveRangeAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default
        );

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default
        );

        TEntity Detach(TEntity entity);
    }
}
