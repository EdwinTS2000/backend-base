using App.Designs.Common.Read;
using App.Interfaces.Common;
using App.Mappings;
using Core.Models;

namespace App.Designs.UserDesign.Read
{
    public class UserGetByIdHandler : GetByIdHandlerBase<User, UserDTO, UserGetByIdQuery>
    {
        public UserGetByIdHandler(IRepositoryBase<User> repository) : base(repository)
        {
        }

        protected override UserDTO MapToDTO(User entity)
        {
            return entity.ToDTO();
        }
    }
}