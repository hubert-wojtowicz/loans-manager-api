using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.BussinesLogic.Dtos.Users;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.BussinesLogic.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IEncypterService _encypterService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(
            IEncypterService encypterService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _encypterService = encypterService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<ViewUserDto>> SelectList(int offset = 0, int take = 15)
        {
            var users = await _userRepository.GetLimited(offset, take);
            return _mapper.Map<IEnumerable<ViewUserDto>>(users);
        }

        public Task<bool> AuthenticateUser(AuthenticateUserDto credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException($"{nameof(credentials)} can not be null.");
            }

            return AuthenticateUserLogic(credentials);
        }

        public async Task<ViewUserDto> FindByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);
            return _mapper.Map<ViewUserDto>(user);
        }

        public async Task<bool> UserExist(string userName, CancellationToken token)
            => await _userRepository.GetByUserName(userName) != null;

        public async Task<bool> UserDoesNotExist(string userName, CancellationToken token)
            => !(await UserExist(userName, token));

        private async Task<bool> AuthenticateUserLogic(AuthenticateUserDto credentials)
        {
            var user = await _userRepository.GetByUserName(credentials.UserName);
            return user != null && _encypterService.GetHash(credentials.Password, user.Salt) == user.Password;
        }
    }
}
