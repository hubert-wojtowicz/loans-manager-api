using LoansManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface ILoansRepository
    {
        Task<List<LoanEntity>> GetLimitedWithLenderAndBorrowerAsync(int offset, int take);
        Task<LoanEntity> GetWithLenderAndBorrowerAync(Guid id);
        Task AddAsync(LoanEntity loanEntity);
        Task<LoanEntity> GetAsync(Guid id);
        Task UpdateAysnc(LoanEntity loan);
        Task<List<LoanEntity>> GetFiltered(Expression<Func<LoanEntity, bool>> predicate, int offset, int take);
        Task<List<dynamic>> GeColumnDistnctAsync(Expression<Func<LoanEntity, dynamic>> selector, int offset, int take);
    }
}
