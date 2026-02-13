namespace Application.Designs.Common.Delete
{
    public class DeleteCommandBase : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
