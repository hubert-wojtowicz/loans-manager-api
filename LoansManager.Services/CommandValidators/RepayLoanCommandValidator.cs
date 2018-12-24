using FluentValidation;
using LoansManager.Services.Commands;
using LoansManager.Services.Resources;
using LoansManager.Services.ServicesContracts;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        Task<bool> LoanExist(Guid loanId, CancellationToken token)
            => loansService.LoanExist(loanId);
    }
}
