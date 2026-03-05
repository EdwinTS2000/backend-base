using App.Designs.Common.Delete;
using FluentValidation;

namespace App.Designs.Common.Read
{
    public class GetFirstValidatorBase<TQuery, TDto>
        : AbstractValidator<TQuery>
        where TQuery : GetFirstQueryBase<TDto>
    {
        public GetFirstValidatorBase()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador es obligatorio.");
        }
    }
}