using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoansManager.DAL.Entities;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface ILoansRepository
    {
        Task<List<LoanEntity>> GetLimitedWithLenderAndBorrower(int offset, int take);

        Task<LoanEntity> GetWithLenderAndBorrower(Guid id);

        Task Add(LoanEntity loanEntity);

        Task<LoanEntity> Get(Guid id);

        Task Update(LoanEntity loan);

        Task<List<LoanEntity>> GetFiltered(Expression<Func<LoanEntity, bool>> predicate, int offset, int take);

        Task<List<dynamic>> GeColumnDistnct(Expression<Func<LoanEntity, dynamic>> selector, int offset, int take);
    }
}
