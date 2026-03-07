using MediatR;

namespace App.Designs.Common.Create
{
    public abstract class CreateCommandBase<TResponse> : IRequest<TResponse>
    {

    }
}