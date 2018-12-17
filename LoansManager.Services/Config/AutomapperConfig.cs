using AutoMapper;
using LoansManager.Domain;
using LoansManager.Services.Dtos;

namespace LoansManager.Services.Config
{
    public static class AutomapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserDto, UserEntity>();
                cfg.CreateMap<ViewUserDto, UserEntity>();
            })
            .CreateMapper();
    }
}
