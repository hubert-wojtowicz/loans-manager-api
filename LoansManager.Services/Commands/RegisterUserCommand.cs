using LoansManager.Services.Infrastructure.CommandsSetup;

namespace LoansManager.CommandHandlers.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
