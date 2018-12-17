using LoansManager.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoansManager.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetLimitedAsync(int offset, int take);
    }
}
