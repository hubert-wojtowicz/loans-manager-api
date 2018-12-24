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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        protected IQueryable<UserEntity> Get()
            => context.Set<UserEntity>();

        public Task<List<UserEntity>> GetLimitedAsync(int offset, int take)
            => Get()
                .OrderBy(x => x.UserName)
                .Skip(offset)
                .Take(take)
                .ToListAsync();

        public Task<UserEntity> GetByUserName(string userName)
            => Get()
                .SingleOrDefaultAsync(x => x.UserName == userName);

        public async Task AddAsync(UserEntity user)
        {
            await context.Set<UserEntity>().AddAsync(user);
            context.SaveChanges();
        }

        public Task<List<UserEntity>> GetFiltered(Expression<Func<UserEntity, bool>> predicate, int offset, int take)
            => context.Users.Where(predicate)
                      .Skip(offset)
                      .Take(take)
                      .ToListAsync();
    }
}
