using Autofac;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _context;
        private readonly FluentValidationFactory validationFactory;

        public CommandBus(IComponentContext context, FluentValidationFactory validationFactory)
        {
            _context = context;
            this.validationFactory = validationFactory;
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

        
        public async Task<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(TCommand).Name}' can not be null.");
            }
            
            var validator = validationFactory.GetValidator(command.GetType());
            return await validator.ValidateAsync(command);
        }
    }
}