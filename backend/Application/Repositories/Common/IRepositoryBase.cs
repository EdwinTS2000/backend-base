using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using core.Models.Common;

namespace Application.Repositories.Common
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        // Task<IDbContextTransaction> BeginTransactionAsync();

        Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>>? predicate = null,
            List<Expression<Func<T, object>>>? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool disableTracking = true
        );

        Task<T> GetAsync(
            Expression<Func<T, bool>>? predicate = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true
        );

        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(List<T> entities);

        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);

        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        T Detach(T entity);
    }
}
