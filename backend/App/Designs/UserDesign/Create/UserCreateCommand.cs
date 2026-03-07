using App.Designs.Common.Create;

namespace App.Designs.UserDesign.Create
{
    public class UserCreateCommand : CreateCommandBase<UserDTO>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}