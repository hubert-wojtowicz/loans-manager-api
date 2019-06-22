using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;

namespace LoansManager.CommandHandlers.Commands.Models
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
