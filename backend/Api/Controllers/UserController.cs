using Api.Controllers.Common;
using App.Designs.UserDesign.Delete;
using Core.Models;
using MediatR;

namespace Api.Controllers
{
    public class UserController : BaseController<UserDeleteCommand>
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
    }
}