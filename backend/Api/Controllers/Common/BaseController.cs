using App.Designs.Common;
using App.Designs.Common.Create;
using App.Designs.Common.Delete;
using App.Designs.Common.Read;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController<
            TResponse,
            TCreateCommand,
            TGetByIdQuery,
            TDeleteCommand
        > : ControllerBase
            where TResponse : IResponseWithId
            where TCreateCommand : IRequest<TResponse>
            where TGetByIdQuery : GetByIdQueryBase<TResponse>, new()
            where TDeleteCommand : DeleteCommandBase, new()
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "Crear entidad")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult<TResponse>> Create(
            [FromBody] TCreateCommand command,
            CancellationToken cancellationToken
        )
        {
            var response = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}", Name = "Obtener entidad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TResponse>> GetById(
            Guid id,
            [FromQuery] bool delete = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = new TDeleteCommand { Id = id, Remove = delete };
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id:guid}", Name = "Eliminar entidad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> Delete(
            Guid id,
            [FromQuery] bool delete = false,
            CancellationToken cancellationToken = default
        )
        {
            var command = new TDeleteCommand { Id = id, Remove = delete };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
