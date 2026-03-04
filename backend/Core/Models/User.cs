using Core.Models.Common;

namespace Core.Models
{
    public class User : EntityBase
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}