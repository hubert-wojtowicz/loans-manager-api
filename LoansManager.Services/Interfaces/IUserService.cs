using LoansManager.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoansManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ViewUserDto>> GetUsersAsync(int offset = 0, int take = 15);
        Task RegisterUserAsync(CreateUserDto createUserDto);
        Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials);
    }
}
