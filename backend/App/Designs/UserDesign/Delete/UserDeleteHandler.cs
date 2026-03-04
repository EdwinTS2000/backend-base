using App.Designs.Common.Delete;
using App.Interfaces.Common;
using Core.Models;

namespace App.Designs.UserDesign.Delete
{
    public class UserDeleteHandler : DeleteHandlerBase<UserDeleteCommand, User>
    {
        public UserDeleteHandler(IRepositoryBase<User> repository) : base(repository)
        {
        }
    }
}