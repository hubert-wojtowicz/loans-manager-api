using System;
using FluentValidation;
using LoansManager.BussinesLogic.Commands.Models;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.BussinesLogic.Resources;

namespace LoansManager.BussinesLogic.Commands.Validators
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator(
            IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException($"{nameof(userService)} can not be null.");
            }

            RuleFor(x => x.LenderId)
                .NotEmpty()
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerNotNullOrEmpty);

            RuleFor(x => x.BorrowerId)
                .NotEmpty()
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerNotNullOrEmpty);

            RuleFor(x => new { x.BorrowerId, x.LenderId })
                .Must(x => x.BorrowerId != x.LenderId)
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerAndLenderMustDiffer);

            RuleFor(x => x.LenderId)
                .MustAsync(userService.UserExist)
                .WithMessage(CreateLoanCommandValidatorResource.LenderDoesNotExtist);

            RuleFor(x => x.BorrowerId)
                .MustAsync(userService.UserExist)
                .WithMessage(CreateLoanCommandValidatorResource.BorrowerDoesNotExist);
        }
    }
}
