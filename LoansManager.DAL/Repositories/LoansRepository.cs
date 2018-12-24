using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public Task<LoanEntity> GetWithLenderAndBorrowerAync(Guid id)
            => Get()
                .Include(x => x.Lender)
                .Include(x => x.Borrower)
                .SingleOrDefaultAsync(x => x.Id == id);
    }
}
