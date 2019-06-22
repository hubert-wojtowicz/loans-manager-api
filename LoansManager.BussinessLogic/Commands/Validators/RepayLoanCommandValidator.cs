using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LoansManager.BussinesLogic.Commands.Models;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.BussinesLogic.Resources;

namespace LoansManager.BussinesLogic.Commands.Validators
{
    public class RepayLoanCommandValidator : AbstractValidator<RepayLoanCommand>
    {
        private readonly ILoansService loansService;

        public RepayLoanCommandValidator(ILoansService loansService)
        {
            this.loansService = loansService;

            RuleFor(x => x.LoanId)
                .MustAsync(LoanExist)
                .WithMessage(RepayLoanCommandValidatorResource.LoanDesNotExist);
        }

        private Task<bool> LoanExist(Guid loanId, CancellationToken token)
            => loansService.LoanExist(loanId);
    }
}
