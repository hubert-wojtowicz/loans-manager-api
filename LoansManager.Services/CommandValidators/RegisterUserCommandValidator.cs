using FluentValidation;
using LoansManager.CommandHandlers.Commands;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Infrastructure.SettingsModels;
using LoansManager.Services.Resources;
using LoansManager.Services.ServicesContracts;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LoansManager.Services.CommandValidators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IUserService userService;

        public RegisterUserCommandValidator(IUserService userService, ApiSettings apiSettings)
        {
            this.userService = userService;

            RuleFor(x => x.Password)
                .Must(x => Regex.IsMatch(x, apiSettings.UserPasswordPattern))
                .WithMessage(RegisterUserCommandValidatorResource.PasswortInvalid);

            RuleFor(x => x.UserName)
                .MustAsync(userService.UserDoesNotExist)
                .WithMessage(RegisterUserCommandValidatorResource.UserExists);
        }
    }
}
