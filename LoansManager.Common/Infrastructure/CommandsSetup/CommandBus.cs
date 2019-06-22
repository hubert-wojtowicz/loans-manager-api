using System;
using System.Threading.Tasks;
using Autofac;
using FluentValidation.Results;

namespace LoansManager.BussinesLogic.Infrastructure.CommandsSetup
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _context;
        private readonly FluentValidationFactory _validationFactory;

        public CommandBus(IComponentContext context, FluentValidationFactory validationFactory)
        {
            _context = context;
            _validationFactory = validationFactory;
        }

        public Task Submit<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(
                    nameof(command),
                    $"Command: '{typeof(TCommand).Name}' can not be null.");
            }

            var handler = _context.Resolve<ICommandHandler<TCommand>>();
            return handler.Handle(command);
        }

        public Task<ValidationResult> Validate<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(
                    nameof(command),
                    $"Command: '{typeof(TCommand).Name}' can not be null.");
            }

            var validator = _validationFactory.GetValidator(command.GetType());
            return validator.ValidateAsync(command);
        }
    }
}