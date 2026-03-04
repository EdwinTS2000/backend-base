using MediatR;

namespace App.Designs.Common.Read
{
    public abstract class GetFirstQueryBase<TDto> : IRequest<TDto>
    {
        public Guid Id { get; set; }
        public bool Delete { get; set; } = false;
    }
}