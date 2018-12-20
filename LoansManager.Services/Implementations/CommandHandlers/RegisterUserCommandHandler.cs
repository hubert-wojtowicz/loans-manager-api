using System.Threading.Tasks;
using LoansManager.CommandHandlers.Commands;
using LoansManager.Services.Infrastructure.CommandsSetup;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.Implementations.CommandHandlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserService userService;
        private readonly IEncypterService encypterService;

        public RegisterUserCommandHandler(
            IUserService userService,
            IEncypterService encypterService)
        {
            this.userService = userService;
            this.encypterService = encypterService;
        }

        public Task HandleAsync(RegisterUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
