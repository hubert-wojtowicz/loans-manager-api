using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoansManager.Services.Dtos;

namespace LoansManager.Services.ServicesContracts
{
    public interface IUserService
    {
        Task<IEnumerable<ViewUserDto>> GetAsync(int offset = 0, int take = 15);

        Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials);

        Task<ViewUserDto> GetAsync(string userName);

        Task<bool> UserExist(string userName, CancellationToken token);

        Task<bool> UserDoesNotExist(string userName, CancellationToken token);
    }
}
