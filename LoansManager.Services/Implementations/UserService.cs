using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Interfaces;

namespace LoansManager.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
    }
}
