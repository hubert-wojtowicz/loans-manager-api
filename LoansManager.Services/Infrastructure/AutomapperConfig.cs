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
                cfg.CreateMap<UserEntity, ViewUserDto>();
                cfg.CreateMap<LoanEntity, ViewLoanDto>()
                    .ForMember(x => x.LenderName, y => y.MapFrom(z => z.Lender.UserName))
                    .ForMember(x => x.BorrowerName, y => y.MapFrom(z => z.Borrower.UserName));
            })
            .CreateMapper();
    }
}
