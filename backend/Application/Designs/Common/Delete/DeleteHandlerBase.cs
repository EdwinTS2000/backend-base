using System.Linq.Expressions;
using Application.Repositories.Common;
using core.Models.Common;
using MediatR;

namespace Application.Designs.Common.Delete
{
    public abstract class DeleteHandlerBase<TCommand, TEntity> :
        IRequestHandler<TCommand, Unit>
        where TEntity : EntityBase
        where TCommand : DeleteCommandBase
    {
        public readonly IRepositoryBase<TEntity> _repository;

        public DeleteHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetEntity(request.Id);
            if (entity == null)
            {
                return Unit.Value;
            }

            await BeforeDeleteAsync(entity, cancellationToken);

            entity.Deleted = _dateTimeHelper.DatePst();
            await _repository.UpdateAsync(entity);

            await AfterDeleteAsync(entity, cancellationToken);

            return Unit.Value;
        }

        protected virtual async Task<TEntity?> GetEntity(Guid id)
        {
            Expression<Func<TEntity, bool>> filter =
                x => x.Deleted == null && x.Id == id;

            return await _repository.GetEntityAsync(filter);
        }

        protected virtual Task BeforeDeleteAsync(
            TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterDeleteAsync(
            TEntity entity, CancellationToken cancellationToken)
             => Task.CompletedTask;
    }
}
