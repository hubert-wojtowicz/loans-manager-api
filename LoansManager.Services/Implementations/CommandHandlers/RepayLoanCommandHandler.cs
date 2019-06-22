using System;
using System.Threading.Tasks;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Commands;
using LoansManager.Services.Infrastructure.CommandsSetup;

namespace LoansManager.Services.Implementations.CommandHandlers
{
    public class RepayLoanCommandHandler : ICommandHandler<RepayLoanCommand>
    {
        private readonly ILoansRepository _loansRepository;

        public RepayLoanCommandHandler(ILoansRepository loansRepository)
        {
            _loansRepository = loansRepository;
        }

        public Task HandleAsync(RepayLoanCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException($"{nameof(command)} can not be null.");
            }

            return Repay(command);
        }

        private async Task Repay(RepayLoanCommand command)
        {
            var loan = await _loansRepository.Get(command.LoanId);
            loan.IsRepaid = true;
            loan.RepaidDate = DateTime.UtcNow;
            await _loansRepository.Update(loan);
        }
    }
}
