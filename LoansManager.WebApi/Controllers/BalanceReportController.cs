using System;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;
using LoansManager.BussinesLogic.Infrastructure.SettingsModels;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.Common.Services;
using LoansManager.WebApi.Controllers.Models.LoansController;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceReportController : ApplicationBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICommandBus _commandBus;
        private readonly ILoansService _loansService;
        private readonly IHttpContextService _uriHelperService;
        private readonly ApiSettings _apiSettings;

        public BalanceReportController(
            IMapper mapper,
            ICommandBus commandBus,
            ILoansService loansService,
            IHttpContextService uriHelperService,
            ApiSettings apiSettings)
        {
            _mapper = mapper;
            _commandBus = commandBus;
            _loansService = loansService;
            _uriHelperService = uriHelperService;
            _apiSettings = apiSettings;
        }

        [HttpPost]
        [ProducesResponseType(202, Type = typeof(Guid))]
        public async Task<IActionResult> Generate(Guid id)
        {
            var loan = await _loansService.Get(id);

            if (loan != null)
            {
                return Ok(_mapper.Map<GetLoanResponseModel>(loan));
            }

            return NotFound(id);
        }
    }
}
