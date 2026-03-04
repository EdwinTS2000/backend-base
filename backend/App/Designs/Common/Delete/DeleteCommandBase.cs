using MediatR;

namespace App.Designs.Common.Delete
{
    public abstract class DeleteCommandBase : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public bool Remove { get; set; } = false;
    }
}
