using System.Threading.Tasks;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
