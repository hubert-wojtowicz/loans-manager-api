using LoansManager.Services.Dtos;
using System;
using System.Threading.Tasks;

namespace LoansManager.Services.ServicesContracts
{
    public interface ILoansService
    {
        Task<ViewLoanDto> GetAsync(Guid id);
    }
}
