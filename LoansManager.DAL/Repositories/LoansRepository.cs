using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories
{
    public class LoansRepository : ILoansRepository
    {
        private readonly ApplicationDbContext context;

        public LoansRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        protected IQueryable<LoanEntity> Get()
            => context.Set<LoanEntity>();

        public Task<List<LoanEntity>> GetLimitedWithLenderAndBorrowerAsync(int offset, int take)
            => Get()
                .Include(x => x.Lender)
                .Include(x => x.Borrower)
                .OrderBy(x => x.StartDate)
                .Skip(offset)
                .Take(take)
                .ToListAsync();

        public Task<LoanEntity> GetWithLenderAndBorrowerAync(Guid id)
            => Get()
                .Include(x => x.Lender)
                .Include(x => x.Borrower)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(LoanEntity loan)
        {
            await context.Set<LoanEntity>().AddAsync(loan);
            await context.SaveChangesAsync();
        }

        public Task<LoanEntity> GetAsync(Guid id)
            => Get()
                .SingleOrDefaultAsync(x => x.Id == id);

        public Task UpdateAysnc(LoanEntity loan)
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

        public Task<List<dynamic>> GeColumnDistnctAsync(Expression<Func<LoanEntity, dynamic>> selector, int offset, int take)
            => Get()
                .OrderBy(selector)
                .Select(selector)
                .Distinct()
                .Skip(offset)
                .Take(take)
                .ToListAsync();
    }
}
