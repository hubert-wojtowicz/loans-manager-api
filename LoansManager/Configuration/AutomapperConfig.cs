using AutoMapper;
using LoansManager.CommandHandlers.Commands;
using LoansManager.DAL.Entities;
using LoansManager.Services.Commands;
using LoansManager.Services.Dtos;

namespace LoansManager.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<RegisterUserCommand, UserEntity>();
            CreateMap<UserEntity, ViewUserDto>();
            CreateMap<LoanEntity, ViewLoanDto>()
                .ForMember(x => x.LenderName, y => y.MapFrom(z => z.Lender.UserName))
                .ForMember(x => x.BorrowerName, y => y.MapFrom(z => z.Borrower.UserName));
            CreateMap<CreateLoanCommand, LoanEntity>();
        }
    }
}
