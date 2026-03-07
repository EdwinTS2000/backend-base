using App.Designs.UserDesign;
using Core.Models;

namespace App.Mappings
{
    public static class UserMapping
    {
        public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}