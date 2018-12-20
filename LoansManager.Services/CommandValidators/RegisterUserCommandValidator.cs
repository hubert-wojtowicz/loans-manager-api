using FluentValidation;
using LoansManager.CommandHandlers.Commands;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LoansManager.Services.CommandValidators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly string eightCharsOneLetterOneNumber = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$";

        public RegisterUserCommandValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(x => x.Password)
                .Must(x => Regex.IsMatch(x, eightCharsOneLetterOneNumber))
                .WithMessage(RegisterUserCommandValidatorResource.PasswortInvalid);

            RuleFor(x => x.UserName)
                .MustAsync(UserDoesNotExist)
                .WithMessage(RegisterUserCommandValidatorResource.UserExists);
        }

        protected async Task<bool> UserDoesNotExist(string userName, CancellationToken token)
            => await userRepository.GetByUserName(userName) == null ? true : false;
    }
}
