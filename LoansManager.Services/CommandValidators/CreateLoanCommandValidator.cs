using FluentValidation;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Commands;
using LoansManager.Services.Resources;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.CommandValidators
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        private readonly IUserService userService;

        public CreateLoanCommandValidator(
            IUserService userService)
        {
            this.userService = userService;
            
            RuleFor(x => x.LenderId)
                .NotNull()
                .NotEmpty()
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerNotNullOrEmpty);

            RuleFor(x => x.BorrowerId)
                .NotNull()
                .NotEmpty()
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerNotNullOrEmpty);

            RuleFor(x => new { x.BorrowerId, x.LenderId })
                .Must(x => x.BorrowerId != x.LenderId)
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerAndLenderMustDiffer);

            RuleFor(x => x.LenderId)
                .MustAsync(userService.UserDoesNotExist)
                .WithMessage(CreateLoanCommandValidatorResource.LenderDoesNotExtist);

            RuleFor(x => x.BorrowerId)
                .MustAsync(userService.UserDoesNotExist)
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerDoesNotExist);
        }
    }
}
