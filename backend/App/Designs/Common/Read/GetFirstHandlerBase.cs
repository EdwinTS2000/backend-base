using System.Linq.Expressions;
using App.Exceptions;
using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.Read
{
    public abstract class GetFirstHandlerBase<TEntity, TDto, TQuery> : IRequestHandler<TQuery, TDto>
    where TEntity : EntityBase
    where TQuery : GetFirstQueryBase<TDto>
    {
        protected readonly IRepositoryBase<TEntity> _repository;
        protected GetFirstHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TDto> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var includes = BuildIncludes();
            var filters = BuildFilters(request);
            var entity = await _repository.GetFirstAsync(filters, includes, true, cancellationToken);

            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, request.Id);

            return MapToDTO(entity);
        }

        protected virtual Expression<Func<TEntity, bool>>? BuildFilters(TQuery request)
        {
            return x => x.Id.Equals(request.Id) && request.Delete || x.DateDelete == null;
        }

        protected virtual List<Expression<Func<TEntity, object>>>? BuildIncludes()
        {
            return null;
        }

        protected abstract TDto MapToDTO(TEntity entity);
    }
}