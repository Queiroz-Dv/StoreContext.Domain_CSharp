using StoreContext.Domain.Commands.Interfaces;

namespace StoreContext.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
