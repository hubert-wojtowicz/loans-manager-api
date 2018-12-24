using System;
using System.Collections.Generic;
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
            ILoansRepository loansRepository
            )
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
    }
}
