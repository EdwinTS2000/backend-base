using FluentValidation;

namespace App.Designs.Common.Delete
{
    public class DeleteValidatorBase<TCommand>
        : AbstractValidator<TCommand>
        where TCommand : DeleteCommandBase
    {
        public DeleteValidatorBase()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador es obligatorio.");
        }
    }
}