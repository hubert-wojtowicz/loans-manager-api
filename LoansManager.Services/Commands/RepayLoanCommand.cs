using LoansManager.Services.Infrastructure.CommandsSetup;
using System;

namespace LoansManager.Services.Commands
{
    public class RepayLoanCommand : ICommand
    {
        public Guid LoanId { get; set; }
    }
}
