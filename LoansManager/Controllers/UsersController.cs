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
    public class UsersController : ApplicationBaseController
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

        /// <summary>
        /// Gets user by its <paramref name="userName"/> key.
        /// </summary>
        /// <param name="userName">Key of concrete user.</param>
        /// <returns>User object.</returns>
        /// <response code="200">When record found.</response>
        /// <response code="404">When no record found.</response> 
        [HttpGet]
        [Route("get/{userName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(string userName)
        {
            var user = string.IsNullOrWhiteSpace(userName) ? null : await userService.GetAsync(userName);

            if (user != null)
                return Ok(user);

            return NotFound(userName);
        }

        /// <summary>
        /// Gets collection of users.
        /// </summary>
        /// <param name="offset">Offset from first record.</param>
        /// <param name="take">Amount of records to take.</param>
        /// <returns>Collection of users objects.</returns>
        /// <response code="200">When at least one record found.</response>
        /// <response code="400">When to many records requested.</response> 
        /// <response code="404">When no records found.</response> 
        [HttpGet]
        [Route("getLimited")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "take")] int take = 15)
        {
            if (take > apiSettings.MaxNumberOfRecordToGet)
                return BadRequest(ValidationResultFactory(nameof(take), take, UserControllerResources.MaxNumberOfRecordToGetExceeded, apiSettings.MaxNumberOfRecordToGet.ToString()));

            var users = await userService.GetAsync(offset, take);
            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        /// <summary>
        /// Adds new user.
        /// </summary>
        /// <param name="createUserDto">Creation of user model.</param>
        /// <response code="201">When record created.</response> 
        /// <response code="404">When validation of <paramref name="createUserDto"/> failed.</response> 
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserCommand createUserDto)
        {
            var validationResult = await commandBus.Validate(createUserDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            await commandBus.Submit(createUserDto);
            return Created($"users/{createUserDto.UserName}", mapper.Map<ViewUserDto>(createUserDto));
        }

        /// <summary>
        /// Generates JWT token for user.
        /// </summary>
        /// <param name="credentials">Credentials model.</param>
        /// <response code="200">When token generated.</response> 
        /// <response code="404">When authentication failed.</response> 
        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateUserDto credentials)
        {
            if (await userService.AuthenticateUserAsync(credentials))
                return Ok(jwtService.GenerateToken(credentials.UserName, Roles.Admin));

            return BadRequest(ValidationResultFactory(nameof(credentials), credentials, UserControllerResources.AuthenticationFailed));
        }
    }
}
