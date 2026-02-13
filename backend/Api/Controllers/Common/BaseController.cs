using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common
{
    [Route("api/v1/[controller]")]
    public class BaseController/* <TDeleteCommand> */ : ControllerBase
    // where TDeleteCommand : DeleteCommandBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpDelete("{id:guid}", Name = "Eliminar entidad")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            // TDeleteCommand query = new() { Id = id };
            await _mediator.Send(query);
            return NoContent();
        }
    }
}