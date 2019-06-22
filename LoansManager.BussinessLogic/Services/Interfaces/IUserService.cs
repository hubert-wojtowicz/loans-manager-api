using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoansManager.BussinesLogic.Dtos.Users;

namespace LoansManager.BussinesLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<ViewUserDto>> SelectList(int offset = 0, int take = 15);

        Task<bool> AuthenticateUser(AuthenticateUserDto credentials);

        Task<ViewUserDto> FindByUserName(string userName);

        Task<bool> UserExist(string userName, CancellationToken token);

        Task<bool> UserDoesNotExist(string userName, CancellationToken token);
    }
}
