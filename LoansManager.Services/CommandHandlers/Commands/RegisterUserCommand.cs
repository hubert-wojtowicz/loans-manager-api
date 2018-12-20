using LoansManager.Services.CommandHandlers.Interface;

namespace LoansManager.CommandHandlers.Commands
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
