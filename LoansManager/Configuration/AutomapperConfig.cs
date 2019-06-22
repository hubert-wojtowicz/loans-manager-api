using AutoMapper;
using LoansManager.BussinesLogic.Commands.Models;
using LoansManager.BussinesLogic.Dtos.Loans;
using LoansManager.BussinesLogic.Dtos.Users;
using LoansManager.CommandHandlers.Commands.Models;
using LoansManager.WebApi.Controllers.Models.LoansController;
using LoansManager.DAL.Entities;

namespace LoansManager.WebApi.Configuration
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
            CreateMap<ViewLoanDto, GetLoanResponseModel>();
        }
    }
}
