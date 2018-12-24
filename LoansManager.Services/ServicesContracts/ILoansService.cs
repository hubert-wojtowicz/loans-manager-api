using LoansManager.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.Services.ServicesContracts
{
    public interface ILoansService
    {
        Task<ViewLoanDto> GetAsync(Guid id);
        Task<IEnumerable<ViewLoanDto>> GetAsync(int offset = 0, int take = 15);
        Task<bool> LoanExist(Guid loanId);
        Task<IEnumerable<ViewLoanBorrowerDto>> GetBorrowersAsync(int offset = 0, int take = 15);
        Task<IEnumerable<ViewLoanLenderDto>> GetLendersAsync(int offset = 0, int take = 15);
    }
}
