using FluentValidation;
using LoansManager.BussinesLogic.Dtos.Users;

namespace LoansManager.WebApi.Validators
{
    public class AuthenticateUserDtoValidator : AbstractValidator<AuthenticateUserDto>
    {
        public AuthenticateUserDtoValidator()
        {
            RuleFor(x => x)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.UserName)
                .NotEmpty();
        }
    }
}
