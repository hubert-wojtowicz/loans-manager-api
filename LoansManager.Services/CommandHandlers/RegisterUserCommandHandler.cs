using System.Threading.Tasks;
using LoansManager.CommandHandlers.Commands;
using LoansManager.Services.CommandHandlers.Interface;
using LoansManager.Services.Interfaces;

namespace LoansManager.Services.CommandHandlers
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
