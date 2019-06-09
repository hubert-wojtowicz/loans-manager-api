using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Dtos;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.Implementations.Services
{
    public class LoansService : ILoansService
    {
        private readonly IMapper mapper;
        private readonly ILoansRepository loansRepository;

        public LoansService(
            IMapper mapper,
            ILoansRepository loansRepository)
        {
            this.mapper = mapper;
            this.loansRepository = loansRepository;
        }

        public async Task<ViewLoanDto> GetAsync(Guid id)
        {
            var loansWithRelatedUsers = await loansRepository.GetWithLenderAndBorrowerAync(id);
            return mapper.Map<ViewLoanDto>(loansWithRelatedUsers);
        }

        public async Task<IEnumerable<ViewLoanDto>> GetAsync(int offset = 0, int take = 15)
        {
            var loans = await loansRepository.GetLimitedWithLenderAndBorrowerAsync(offset, take);
            return mapper.Map<IEnumerable<ViewLoanDto>>(loans);
        }

        public async Task<IEnumerable<ViewLoanBorrowerDto>> GetBorrowersAsync(int offset = 0, int take = 15)
        {
            var borrowers = await loansRepository.GeColumnDistnctAsync(x => x.LenderId, offset, take);
            return borrowers.Select(x => new ViewLoanBorrowerDto { BorrowerId = x.ToString() });
        }

        public async Task<IEnumerable<ViewLoanLenderDto>> GetLendersAsync(int offset = 0, int take = 15)
        {
            var lenders = await loansRepository.GeColumnDistnctAsync(x => x.LenderId, offset, take);
            return lenders.Select(x => new ViewLoanLenderDto { LenderId = x.ToString() });
        }

        public async Task<IEnumerable<ViewLoanDto>> GetUserLoansAsync(string userId, int offset = 0, int take = 15)
        {
            var borrowedLoans = await loansRepository.GetFiltered(x => x.BorrowerId == userId, offset, take);
            return mapper.Map<IEnumerable<ViewLoanDto>>(borrowedLoans);
        }

        public async Task<bool> LoanExist(Guid loanId)
            => await loansRepository.GetAsync(loanId) == null;
    }
}
