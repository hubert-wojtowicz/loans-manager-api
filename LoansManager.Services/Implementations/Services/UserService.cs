using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Dtos;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.Implementations.Services
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

        public async Task<IEnumerable<ViewUserDto>> GetAsync(int offset = 0, int take = 15)
        {
            var users = await userRepository.GetLimitedAsync(offset, take);
            return mapper.Map<IEnumerable<ViewUserDto>>(users);
        }

        public async Task<bool> AuthenticateUserAsync(AuthenticateUserDto credentials)
        {
            var user = await userRepository.GetByUserName(credentials.UserName);
            return user != null && encypterService.GetHash(credentials.Password, user.Salt) == user.Password ? true : false;
        }

        public async Task<ViewUserDto> GetAsync(string userName)
        {
            var user = await userRepository.GetByUserName(userName);
            return mapper.Map<ViewUserDto>(user);
        }
    }
}
