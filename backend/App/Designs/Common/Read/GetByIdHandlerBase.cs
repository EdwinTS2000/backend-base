using System.Linq.Expressions;
using App.Exceptions;
using App.Extensions;
using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.Read
{
    public abstract class GetByIdHandlerBase<TEntity, TDto, TQuery> : IRequestHandler<TQuery, TDto>
    where TEntity : EntityBase
    where TQuery : GetByIdQueryBase<TDto>
    {
        protected readonly IRepositoryBase<TEntity> _repository;
        protected GetByIdHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TDto> Handle(TQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<TEntity, bool>> filters = x =>
                x.Id == request.Id
                && (request.Delete || x.DateDelete == null);

            var extraFilters = BuildFilters(request);
            if (extraFilters != null)
                filters = filters.And(extraFilters);

            var includes = BuildIncludes();

            var entity = await GetEntity(filters, includes, cancellationToken);

            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, request.Id);

            return MapToDTO(entity);
        }

        protected virtual Expression<Func<TEntity, bool>>? BuildFilters(TQuery request)
            => null;

        protected virtual List<Expression<Func<TEntity, object>>>? BuildIncludes()
        {
            return null;
        }

        protected async virtual Task<TEntity?> GetEntity(
            Expression<Func<TEntity, bool>>? filters,
            List<Expression<Func<TEntity, object>>>? includes,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetFirstAsync(filters, includes, true, cancellationToken);
        }

        protected abstract TDto MapToDTO(TEntity entity);
    }
}