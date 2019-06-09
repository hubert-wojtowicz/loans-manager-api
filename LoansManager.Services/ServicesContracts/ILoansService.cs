using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoansManager.Services.Dtos;

namespace LoansManager.Services.ServicesContracts
{
    public interface ILoansService
    {
        Task<ViewLoanDto> GetAsync(Guid id);

        Task<IEnumerable<ViewLoanDto>> GetAsync(int offset = 0, int take = 15);

        Task<bool> LoanExist(Guid loanId);

        Task<IEnumerable<ViewLoanBorrowerDto>> GetBorrowersAsync(int offset = 0, int take = 15);

        Task<IEnumerable<ViewLoanLenderDto>> GetLendersAsync(int offset = 0, int take = 15);

        Task<IEnumerable<ViewLoanDto>> GetUserLoansAsync(string userId, int offset = 0, int take = 15);
    }
}
