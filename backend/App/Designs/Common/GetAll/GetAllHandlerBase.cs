using System.Linq.Expressions;
using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.GetAll
{
    public abstract class GetAllHandlerBase<TEntity, TDto, TQuery>
        : IRequestHandler<TQuery, DataCollection<TDto>>
        where TEntity : EntityBase
        where TDto : IResponseWithId
        where TQuery : GetAllQueryBase<TDto>
    {
        private readonly IRepositoryBase<TEntity> _repository;
        protected GetAllHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<DataCollection<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var take = request.Take > 0 ? request.Take : 50;
            var filter = BuildFilter(request);
            var order = BuildOrder();
            var includes = BuildIncludes();
            var data = await _repository.ToListAsync(filter, includes, order, true, cancellationToken);
            var items = data
                .Skip(request.Skip)
                .Take(take)
                .Select(MapToDto)
                .ToList();

            return new DataCollection<TDto>
            {
                Items = items,
                Total = data.Count,
                Page = (request.Skip / take) + 1,
                PageSize = take
            };

        }
        protected abstract Expression<Func<TEntity, bool>> BuildFilter(TQuery request);
        protected abstract Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> BuildOrder();
        protected virtual List<Expression<Func<TEntity, object>>>? BuildIncludes()
        {
            return null;
        }
        protected abstract TDto MapToDto(TEntity entity);
    }
}