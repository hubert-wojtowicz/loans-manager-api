using AutoMapper;
using LoansManager.CommandHandlers.Commands;
using LoansManager.Domain;
using LoansManager.Services.Dtos;

namespace LoansManager.Services.Infrastructure
{
    public static class AutomapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserCommand, UserEntity>();
                cfg.CreateMap<ViewUserDto, UserEntity>();
            })
            .CreateMapper();
    }
}
