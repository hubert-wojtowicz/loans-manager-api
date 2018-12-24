using LoansManager.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface ILoansRepository
    {
        Task<List<LoanEntity>> GetLimitedWithLenderAndBorrowerAsync(int offset, int take);
        Task<LoanEntity> GetWithLenderAndBorrowerAync(Guid id);
        Task AddAsync(LoanEntity loanEntity);
    }
}
