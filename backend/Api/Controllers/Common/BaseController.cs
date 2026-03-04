using App.Designs.Common.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseController<TDeleteCommand> :
        ControllerBase
        where TDeleteCommand : DeleteCommandBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpDelete(Name = "Eliminar Cualquier entidad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Unit>> Delete(
            TDeleteCommand command,
            CancellationToken cancellationToken
        )
        {
            Console.WriteLine("edwin");
            Console.WriteLine(command.Id);
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}