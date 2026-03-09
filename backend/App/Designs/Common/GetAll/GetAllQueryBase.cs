using MediatR;

namespace App.Designs.Common.GetAll
{
    public abstract class GetAllQueryBase<TDto> : IRequest<DataCollection<TDto>>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 50;
    }
}