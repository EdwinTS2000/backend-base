using System.Linq.Expressions;
using App.Exceptions;
using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.Update
{
    public abstract class UpdateHandlerBase<TEntity, TCommand>
        : IRequestHandler<TCommand, Unit>
        where TEntity : EntityBase
        where TCommand : UpdateCommandBase
    {
        protected readonly IRepositoryBase<TEntity> _repository;
        protected UpdateHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
        {
            Expression<Func<TEntity, bool>> filters = x =>
                x.Id == command.Id
                && (command.Delete || x.DateDelete == null);

            var entity = await _repository.GetFirstAsync(filters, null, false, cancellationToken);

            if (entity == null)
                throw new NotFoundException(typeof(TEntity).Name, command.Id);

            await BeforeUpdateAsync(entity, command, cancellationToken);

            ApplyUpdated(command, entity);
            entity.DateUpdate = DateTime.UtcNow;

            await AfterUpdateAsync(entity, command, cancellationToken);

            await _repository.UpdateAsync(entity);
            return Unit.Value;
        }

        protected abstract void ApplyUpdated(TCommand command, TEntity entity);

        protected virtual Task BeforeUpdateAsync(
            TEntity entity, TCommand command, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterUpdateAsync(
            TEntity entity, TCommand command, CancellationToken cancellationToken)
             => Task.CompletedTask;
    }
}