using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.BussinesLogic.Dtos.Loans;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.BussinesLogic.Implementations.Services
{
    public class LoansService : ILoansService
    {
        private readonly IMapper _mapper;
        private readonly ILoansRepository _loansRepository;

        public LoansService(
            IMapper mapper,
            ILoansRepository loansRepository)
        {
            _mapper = mapper;
            _loansRepository = loansRepository;
        }

        public async Task<ViewLoanDto> Get(Guid id)
        {
            var loansWithRelatedUsers = await _loansRepository.GetWithLenderAndBorrower(id);
            return _mapper.Map<ViewLoanDto>(loansWithRelatedUsers);
        }

        public async Task<IEnumerable<ViewLoanDto>> Get(int offset = 0, int take = 15)
        {
            var loans = await _loansRepository.GetLimitedWithLenderAndBorrower(offset, take);
            return _mapper.Map<IEnumerable<ViewLoanDto>>(loans);
        }

        public async Task<IEnumerable<ViewLoanBorrowerDto>> GetBorrowers(int offset = 0, int take = 15)
        {
            var borrowers = await _loansRepository.GeColumnDistnct(x => x.LenderId, offset, take);
            return borrowers.Select(x => new ViewLoanBorrowerDto { BorrowerId = x.ToString() });
        }

        public async Task<IEnumerable<ViewLoanLenderDto>> GetLenders(int offset = 0, int take = 15)
        {
            var lenders = await _loansRepository.GeColumnDistnct(x => x.LenderId, offset, take);
            return lenders.Select(x => new ViewLoanLenderDto { LenderId = x.ToString() });
        }

        public async Task<IEnumerable<ViewLoanDto>> GetUserLoans(string userId, int offset = 0, int take = 15)
        {
            var borrowedLoans = await _loansRepository.GetFiltered(x => x.BorrowerId == userId, offset, take);
            return _mapper.Map<IEnumerable<ViewLoanDto>>(borrowedLoans);
        }

        public async Task<bool> LoanExist(Guid loanId)
            => await _loansRepository.Get(loanId) == null;
    }
}
