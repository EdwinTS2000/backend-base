using App.Interfaces.Common;
using Core.Models.Common;
using MediatR;

namespace App.Designs.Common.Create
{
    public abstract class CreateHandlerBase<TEntity, TResponse, TCommand>
        : IRequestHandler<TCommand, TResponse>
        where TEntity : EntityBase, new()
        where TResponse : IResponseWithId
        where TCommand : CreateCommandBase<TResponse>
    {
        protected readonly IRepositoryBase<TEntity> _repository;

        public CreateHandlerBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken)
        {
            await BeforeCreateAsync(command, cancellationToken);

            var entity = await CreateEntityAsync(command);

            await AfterCreateAsync(entity, command, cancellationToken);

            var result = await _repository.AddAsync(entity);
            return MapToDTO(result);
        }

        protected virtual async Task<TEntity> CreateEntityAsync(TCommand command)
        {
            var entity = new TEntity
            {
                Id = Guid.NewGuid(),
                DateCreate = DateTime.UtcNow
            };
            await MapToEntity(command, entity);

            return entity;
        }

        protected abstract Task MapToEntity(TCommand command, TEntity entity);
        protected abstract TResponse MapToDTO(TEntity entity);

        protected virtual Task BeforeCreateAsync(
            TCommand command, CancellationToken cancellationToken)
            => Task.CompletedTask;

        protected virtual Task AfterCreateAsync(
            TEntity entity, TCommand command, CancellationToken cancellationToken)
             => Task.CompletedTask;
    }
}