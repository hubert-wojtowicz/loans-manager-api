using System.Threading.Tasks;
using AutoMapper;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Domain;
using LoansManager.Services.Commands;
using LoansManager.Services.Infrastructure.CommandsSetup;

namespace LoansManager.Services.Implementations.CommandHandlers
{
    public class CreateLoanCommandHandler : ICommandHandler<CreateLoanCommand>
    {
        private readonly ILoansRepository loansRepository;
        private readonly IMapper mapper;

        public CreateLoanCommandHandler(
            ILoansRepository loansRepository,
            IMapper mapper
            )
        {
            this.loansRepository = loansRepository;
            this.mapper = mapper;
        }

        public Task HandleAsync(CreateLoanCommand command)
        {
            var loan = mapper.Map<LoanEntity>(command);
            return loansRepository.AddAsync(loan);
        }
    }
}
