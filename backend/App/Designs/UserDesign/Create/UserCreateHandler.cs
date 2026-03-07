using App.Designs.Common.Create;
using App.Interfaces.Common;
using App.Mappings;
using Core.Models;

namespace App.Designs.UserDesign.Create
{
    public class UserCreateHandler : CreateHandlerBase<User, UserDTO, UserCreateCommand>
    {
        public UserCreateHandler(IRepositoryBase<User> repository) : base(repository)
        {
        }

        protected override UserDTO MapToDTO(User entity)
        {
            return entity.ToDTO();
        }

        protected override async Task MapToEntity(UserCreateCommand command, User entity)
        {
            entity.Email = command.Email;
            entity.Password = command.Password;
        }
    }
}