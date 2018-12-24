using LoansManager.Domain;
using System;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface ILoansRepository
    {
        Task<LoanEntity> GetWithLenderAndBorrowerAync(Guid id);
        Task AddAsync(LoanEntity loanEntity);
    }
}
