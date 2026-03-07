using MediatR;

namespace App.Designs.Common.Update
{
    public abstract class UpdateCommandBase : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public bool Delete { get; set; } = false;
    }
}