using LoansManager.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.Services.ServicesContracts
{
    public interface IUserService
    {
        Task<IEnumerable<ViewUserDto>> GetUsersAsync(int offset = 0, int take = 15);
        Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials);
    }
}
