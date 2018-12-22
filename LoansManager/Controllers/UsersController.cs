using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.CommandHandlers.Commands;
using LoansManager.Domain;
using LoansManager.Resources;
using LoansManager.Services.Dtos;
using LoansManager.Services.Infrastructure.CommandsSetup;
using LoansManager.Services.Infrastructure.SettingsModels;
using LoansManager.Services.ServicesContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiSettings apiSettings;
        private readonly IMapper mapper;
        private readonly IJwtService jwtService;
        private readonly IUserService userService;
        private readonly ICommandBus commandBus;

        public UsersController(
            ApiSettings apiSettings,
            IMapper mapper,
            IJwtService jwtService,
            IUserService userService,
            ICommandBus commandBus
            )
        {
            this.apiSettings = apiSettings;
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userService = userService;
            this.commandBus = commandBus;
        }

        [HttpGet]
        [Route("get/{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = string.IsNullOrWhiteSpace(userName) ? null : await userService.GetAsync(userName);
            if (user == null) 
                BadRequest(string.Format(UserControllerResources.NoUserExists, userName));

            return Ok(user);
        }

        [HttpGet]
        [Route("getLimited")]
        public async Task<IActionResult> GetManyUsersAsync([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "take")] int take = 15)
        {
            if (take > apiSettings.MaxNumberOfRecordToGet)
                return BadRequest(UserControllerResources.MaxNumberOfRecordToGetExceeded);

            var users = await userService.GetAsync(offset, take);

            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody]RegisterUserCommand createUserDto)
        {
            var validationResult = await commandBus.Validate(createUserDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            await commandBus.Submit(createUserDto);
            return Created($"users/get/{createUserDto.UserName}", mapper.Map<ViewUserDto>(createUserDto));
        }

        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateUserDto credentials)
        {
            if (await userService.AuthenticateUserAsync(credentials))
            {
                var token = jwtService.GenerateToken(credentials.UserName, Roles.Admin);
                return Ok(token);
            }

            return BadRequest(UserControllerResources.AuthenticationFailed);
        }
    }
}
