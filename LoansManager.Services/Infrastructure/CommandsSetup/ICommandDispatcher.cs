using LoansManager.Services.CommandHandlers.Interface;
using System.Threading.Tasks;

namespace LoansManager.Services.CommandHandlers.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task Submit<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
