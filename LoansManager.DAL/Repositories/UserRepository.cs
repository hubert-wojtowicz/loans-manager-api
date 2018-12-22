using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        protected IQueryable<UserEntity> Get()
            => context.Set<UserEntity>();

        public async Task<IEnumerable<UserEntity>> GetLimitedAsync(int offset, int take)
            => await Get()
                .OrderBy(x => x.UserName)
                .Skip(offset)
                .Take(take)
                .ToListAsync();

        public async Task<UserEntity> GetByUserName(string userName)
            => await Get()
                .SingleOrDefaultAsync(x => x.UserName == userName);

        public async Task AddAsync(UserEntity user)
        {
            await context.Set<UserEntity>().AddAsync(user);
            context.SaveChanges();
        }
    }
}
