using System;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;

namespace LoansManager.BussinesLogic.Commands.Models
{
    public class RepayLoanCommand : ICommand
    {
        public Guid LoanId { get; set; }
    }
}
