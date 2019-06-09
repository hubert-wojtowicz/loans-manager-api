using System;
using LoansManager.Services.Infrastructure.CommandsSetup;

namespace LoansManager.Services.Commands
{
    public class RepayLoanCommand : ICommand
    {
        public Guid LoanId { get; set; }
    }
}
