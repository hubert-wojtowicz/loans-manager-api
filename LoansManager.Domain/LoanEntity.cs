using System;

namespace LoansManager.Domain
{
    public class LoanEntity
    {
        public Guid Id { get; set; }

        public decimal CommitmentValue { get; set; }

        public bool IsRepaid { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? RepaidDate { get; set; }

        public string LenderId { get; set; }

        public UserEntity Lender { get; set; }

        public string BorrowerId { get; set; }

        public UserEntity Borrower { get; set; }
    }
}
