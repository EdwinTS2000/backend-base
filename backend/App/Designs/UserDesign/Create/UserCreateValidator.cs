using App.Designs.Common.Delete;
using FluentValidation;

namespace App.Designs.UserDesign.Create
{
    public class UserCreateValidator
        : AbstractValidator<UserCreateCommand>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio")
                .MaximumLength(25).WithMessage("El correo no debe superar los 25 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MaximumLength(25).WithMessage("La contraseña no debe superar los 25 caracteres");
        }
    }
}