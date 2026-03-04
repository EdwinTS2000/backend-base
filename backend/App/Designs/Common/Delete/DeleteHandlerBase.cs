using System.Linq.Expressions;
using App.Exceptions;
using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.Delete
{
    public abstract class DeleteHandlerBase<TCommand, TEntity> :
        IRequestHandler<TCommand, Unit>
        where TCommand : DeleteCommandBase
        where TEntity : EntityBase
    {
        protected readonly IRepositoryBase<TEntity> _repository;

        public DeleteHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetEntity(request.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, request.Id);

            await BeforeDeleteAsync(entity, cancellationToken);

            await ApplyDelete(request, entity, cancellationToken);

            await AfterDeleteAsync(entity, cancellationToken);

            return Unit.Value;
        }

        protected virtual async Task<TEntity?> GetEntity(Guid id, CancellationToken cancellationToken)
        {
            Expression<Func<TEntity, bool>> filter =
                x => x.DateDelete == null && x.Id == id;

            return await _repository.GetFirstAsync(filter, null, true, cancellationToken);
        }

        protected async virtual Task ApplyDelete(TCommand request, TEntity entity, CancellationToken cancellationToken)
        {
            if (!request.Remove)
            {
                entity.DateDelete = DateTime.UtcNow;
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            await _repository.RemoveAsync(entity, cancellationToken);
        }

        protected virtual Task BeforeDeleteAsync(
            TEntity entity, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterDeleteAsync(
            TEntity entity, CancellationToken cancellationToken)
             => Task.CompletedTask;
    }
}
