using System.Threading.Tasks;
using AutoMapper;
using LoansManager.BussinesLogic.Commands.Models;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;
using LoansManager.DAL.Entities;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.BussinesLogic.Commands.Handlers
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

        public Task Handle(CreateLoanCommand command)
        {
            var loan = _mapper.Map<LoanEntity>(command);
            return _loansRepository.Add(loan);
        }
    }
}
