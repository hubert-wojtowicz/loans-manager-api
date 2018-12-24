using LoansManager.Services.Infrastructure.CommandsSetup;
using System;

namespace LoansManager.Services.Commands
{
    public class CreateLoanCommand : ICommand
    {
        public Guid Id { get; set; }
        public decimal CommitmentValue { get; set; }
        public DateTime StartDate { get; set; }
        public string LenderId { get; set; }
        public string BorrowerId { get; set; }
    }
}
