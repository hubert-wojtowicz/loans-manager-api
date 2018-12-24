using LoansManager.Resources;
using LoansManager.Services.Infrastructure.CommandsSetup;
using LoansManager.Services.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LoansManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ApplicationBaseController
    {
        private readonly ICommandBus commandBus;
        private readonly ILoansService loansService;

        public LoansController(
            ICommandBus commandBus,
            ILoansService loansService
            )
        {
            this.commandBus = commandBus;
            this.loansService = loansService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var loan = await loansService.GetAsync(id);

            if (loan == null)
                return BadRequest(ValidationResultFactory(nameof(id), id, LoansControllerResources.LoanDoesNotExist, id.ToString()));
            
            return Ok(loan);
        }
    }
}
