using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LoansManager.Services.Commands;
using LoansManager.Services.Resources;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.CommandValidators
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
