using System.Threading.Tasks;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public interface ICommandDispatcher
    {
        Task Submit<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
