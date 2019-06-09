using System.Threading.Tasks;
using FluentValidation.Results;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public interface ICommandBus
    {
        Task Submit<TCommand>(TCommand command)
            where TCommand : ICommand;

        Task<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
