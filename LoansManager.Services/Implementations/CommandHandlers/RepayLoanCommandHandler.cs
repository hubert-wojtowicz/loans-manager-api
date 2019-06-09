using System;
using System.Threading.Tasks;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Commands;
using LoansManager.Services.Infrastructure.CommandsSetup;

namespace LoansManager.Services.Implementations.CommandHandlers
{
    public class RepayLoanCommandHandler : ICommandHandler<RepayLoanCommand>
    {
        private readonly ILoansRepository loansRepository;

        public RepayLoanCommandHandler(ILoansRepository loansRepository)
        {
            this.loansRepository = loansRepository;
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
            var loan = await loansRepository.GetAsync(command.LoanId);
            loan.IsRepaid = true;
            loan.RepaidDate = DateTime.UtcNow;
            await loansRepository.UpdateAysnc(loan);
        }
    }
}
