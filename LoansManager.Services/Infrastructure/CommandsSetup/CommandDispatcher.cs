using Autofac;
using System;
using System.Threading.Tasks;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task Submit<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(TCommand).Name}' can not be null.");
            }
            var handler = _context.Resolve<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}
