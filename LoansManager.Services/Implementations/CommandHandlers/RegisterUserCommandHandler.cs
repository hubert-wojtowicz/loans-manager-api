using System.Threading.Tasks;
using AutoMapper;
using LoansManager.CommandHandlers.Commands;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Domain;
using LoansManager.Services.Infrastructure.CommandsSetup;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.Implementations.CommandHandlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IEncypterService encypterService;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public RegisterUserCommandHandler(
            IEncypterService encypterService,
            IMapper mapper,
            IUserRepository userRepository
            )
        {
            this.encypterService = encypterService;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public Task HandleAsync(RegisterUserCommand command)
        {
            var salt = encypterService.GetSalt(command.Password);
            var passwordHash = encypterService.GetHash(command.Password, salt);
            var user = mapper.Map<UserEntity>(command);
            user.Salt = salt;
            user.Password = passwordHash;

            return userRepository.AddAsync(user);
        }
    }
}
