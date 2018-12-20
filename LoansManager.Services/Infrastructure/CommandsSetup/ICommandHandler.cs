using System.Threading.Tasks;

namespace LoansManager.Services.CommandHandlers.Interface
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
