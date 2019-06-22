using System;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.CommandHandlers.Commands.Models;
using LoansManager.DAL.Entities;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.BussinesLogic.Commands.Handlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IEncypterService _encypterService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(
            IEncypterService encypterService,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _encypterService = encypterService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Task Handle(RegisterUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException($"{nameof(command)} can not be null.");
            }

            var salt = _encypterService.GetSalt(command.Password);
            var passwordHash = _encypterService.GetHash(command.Password, salt);
            var user = _mapper.Map<UserEntity>(command);
            user.Salt = salt;
            user.Password = passwordHash;

            return _userRepository.Add(user);
        }
    }
}
