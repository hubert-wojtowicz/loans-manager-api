using LoansManager.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetLimitedAsync(int offset, int take);
        Task<UserEntity> GetByUserName(string userName);
        Task AddAsync(UserEntity user);
    }
}
