using System;
using System.Threading.Tasks;
using LoansManager.BussinesLogic.Commands.Models;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.BussinesLogic.Commands.Handlers
{
    public class RepayLoanCommandHandler : ICommandHandler<RepayLoanCommand>
    {
        private readonly ILoansRepository _loansRepository;

        public RepayLoanCommandHandler(ILoansRepository loansRepository)
        {
            _loansRepository = loansRepository;
        }

        public Task Handle(RepayLoanCommand command)
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
