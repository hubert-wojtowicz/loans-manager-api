using System.Threading.Tasks;

namespace LoansManager.BussinesLogic.Infrastructure.CommandsSetup
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
