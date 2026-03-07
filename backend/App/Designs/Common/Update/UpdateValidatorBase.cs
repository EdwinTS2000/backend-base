using FluentValidation;

namespace App.Designs.Common.Update
{
    public class DeleteValidatorBase<TCommand>
        : AbstractValidator<TCommand>
        where TCommand : UpdateCommandBase
    {
        public DeleteValidatorBase()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador de la entidad es obligatorio.");
        }
    }
}