using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoansManager.Services.Dtos;

namespace LoansManager.Services.ServicesContracts
{
    public interface ILoansService
    {
        Task<ViewLoanDto> Get(Guid id);

        Task<IEnumerable<ViewLoanDto>> Get(int offset = 0, int take = 15);

        Task<bool> LoanExist(Guid loanId);

        Task<IEnumerable<ViewLoanBorrowerDto>> GetBorrowers(int offset = 0, int take = 15);

        Task<IEnumerable<ViewLoanLenderDto>> GetLenders(int offset = 0, int take = 15);

        Task<IEnumerable<ViewLoanDto>> GetUserLoans(string userId, int offset = 0, int take = 15);
    }
}
