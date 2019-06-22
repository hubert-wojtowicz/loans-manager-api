using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LoansManager.DAL.Entities;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetLimited(int offset, int take);

        Task<UserEntity> GetByUserName(string userName);

        Task Add(UserEntity user);

        Task<List<UserEntity>> GetFiltered(Expression<Func<UserEntity, bool>> predicate, int offset, int take);
    }
}
