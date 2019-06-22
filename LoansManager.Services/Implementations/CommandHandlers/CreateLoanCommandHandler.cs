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
        private readonly ILoansRepository _loansRepository;
        private readonly IMapper _mapper;

        public CreateLoanCommandHandler(
            ILoansRepository loansRepository,
            IMapper mapper)
        {
            _loansRepository = loansRepository;
            _mapper = mapper;
        }

        public Task HandleAsync(CreateLoanCommand command)
        {
            var loan = _mapper.Map<LoanEntity>(command);
            return _loansRepository.Add(loan);
        }
    }
}
