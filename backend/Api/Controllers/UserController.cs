using Api.Controllers.Common;
using App.Designs.UserDesign;
using App.Designs.UserDesign.Create;
using App.Designs.UserDesign.Delete;
using App.Designs.UserDesign.Read;
using Core.Models;
using MediatR;

namespace Api.Controllers
{
    public class UserController : BaseController<
        UserDTO,
        UserCreateCommand,
        UserGetByIdQuery,
        UserDeleteCommand
    >
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
    }
}