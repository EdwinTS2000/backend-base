using App.Designs.Common.Delete;
using FluentValidation;

namespace App.Designs.Common.Read
{
    public class GetByIdValidatorBase<TQuery, TDto>
        : AbstractValidator<TQuery>
        where TQuery : GetByIdQueryBase<TDto>
    {
        public GetByIdValidatorBase()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El identificador es obligatorio.");
        }
    }
}