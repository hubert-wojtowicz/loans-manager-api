using FluentValidation.Results;
using System.Threading.Tasks;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public interface ICommandBus
    {
        Task Submit<TCommand>(TCommand command) where TCommand : ICommand;
        Task<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
