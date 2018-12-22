using LoansManager.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.Services.ServicesContracts
{
    public interface IUserService
    {
        Task<IEnumerable<ViewUserDto>> GetAsync(int offset = 0, int take = 15);
        Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials);
        Task<ViewUserDto> GetAsync(string userName);
    }
}
