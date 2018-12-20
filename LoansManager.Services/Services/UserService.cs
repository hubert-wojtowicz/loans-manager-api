using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Dtos;
using LoansManager.Services.Interfaces;

namespace LoansManager.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IEncypterService encypterService;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserService(
            IEncypterService encypterService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.encypterService = encypterService;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<ViewUserDto>> GetUsersAsync(int offset = 0, int take = 15)
        {
            var users = await userRepository.GetLimitedAsync(offset, take);
            return mapper.Map<IEnumerable<ViewUserDto>>(users);
        }

        public async Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials)
        {
            var user = await userRepository.GetByUserName(credentials.UserName);

            return user == null || encypterService.GetHash(credentials.Password, user.Salt) == user.Password ? false : true;
        }
    }
}
