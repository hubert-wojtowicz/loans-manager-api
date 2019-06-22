using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoansManager.DAL.Entities;
using LoansManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoansManager.DAL.Repositories
{
    public class LoansRepository : ILoansRepository
    {
        private readonly ApplicationDbContext context;

        public LoansRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<LoanEntity>> GetLimitedWithLenderAndBorrower(int offset, int take)
            => Get()
                .Include(x => x.Lender)
                .Include(x => x.Borrower)
                .OrderBy(x => x.StartDate)
                .Skip(offset)
                .Take(take)
                .ToListAsync();

        public Task<LoanEntity> GetWithLenderAndBorrower(Guid id)
            => Get()
                .Include(x => x.Lender)
                .Include(x => x.Borrower)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task Add(LoanEntity loanEntity)
        {
            await context.Set<LoanEntity>().AddAsync(loanEntity);
            await context.SaveChangesAsync();
        }

        public Task<LoanEntity> Get(Guid id)
            => Get()
                .SingleOrDefaultAsync(x => x.Id == id);

        public Task Update(LoanEntity loan)
        {
            context.Loans.Update(loan);
            return context.SaveChangesAsync();
        }

        public Task<List<LoanEntity>> GetFiltered(Expression<Func<LoanEntity, bool>> predicate, int offset, int take)
            => context.Loans
                      .Where(predicate)
                      .Skip(offset)
                      .Take(take)
                      .ToListAsync();

        public Task<List<dynamic>> GeColumnDistnct(Expression<Func<LoanEntity, dynamic>> selector, int offset, int take)
            => Get()
                .OrderBy(selector)
                .Select(selector)
                .Distinct()
                .Skip(offset)
                .Take(take)
                .ToListAsync();

        protected IQueryable<LoanEntity> Get()
            => context.Set<LoanEntity>();
    }
}
