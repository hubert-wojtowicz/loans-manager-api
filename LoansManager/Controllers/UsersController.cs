using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LoansManager.BussinesLogic.Dtos.Users;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;
using LoansManager.BussinesLogic.Infrastructure.SettingsModels;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.CommandHandlers.Commands.Models;
using LoansManager.DAL.Entities;
using LoansManager.WebApi.Helper;
using LoansManager.WebApi.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApplicationBaseController
    {
        private readonly AbstractValidator<AuthenticateUserDto> _authenticateUserDtoValidator;
        private readonly ApiSettings _apiSettings;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IUriHelperService _uriHelperService;
        private readonly IUserService _userService;
        private readonly ICommandBus _commandBus;

        public UsersController(
            AbstractValidator<AuthenticateUserDto> authenticateUserDtoValidator,
            ApiSettings apiSettings,
            IMapper mapper,
            IJwtService jwtService,
            IUriHelperService uriHelperService,
            IUserService userService,
            ICommandBus commandBus)
        {
            _authenticateUserDtoValidator = authenticateUserDtoValidator;
            _apiSettings = apiSettings;
            _mapper = mapper;
            _jwtService = jwtService;
            _uriHelperService = uriHelperService;
            _userService = userService;
            _commandBus = commandBus;
        }

        /// <summary>
        /// Gets user by its <paramref name="userName"/> key.
        /// </summary>
        /// <param name="userName">Key of concrete user.</param>
        /// <returns>User object.</returns>
        /// <response code="200">When record found.</response>
        /// <response code="404">When no record found.</response>
        [HttpGet]
        [Route("{userName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string userName)
        {
            var user = string.IsNullOrWhiteSpace(userName) ? null : await _userService.FindByUserName(userName);

            if (user != null)
            {
                return Ok(user);
            }

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "take")] int take = 15)
        {
            if (take > _apiSettings.MaxNumberOfRecordToGet)
            {
                return BadRequest(
                    ValidationResultFactory(
                        nameof(take),
                        take,
                        UserControllerResources.MaxNumberOfRecordToGetExceeded,
                        _apiSettings.MaxNumberOfRecordToGet.ToString(CultureInfo.InvariantCulture)));
            }

            var users = await _userService.SelectList(offset, take);
            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }

        /// <summary>
        /// Adds new user.
        /// </summary>
        /// <param name="createUserDto">Creation of user model.</param>
        /// <response code="201">When record created.</response>
        /// <response code="404">When validation of <paramref name="createUserDto"/> failed.</response>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand createUserDto)
        {
            var validationresult = await _commandBus.Validate(createUserDto);
            if (!validationresult.IsValid)
            {
                return BadRequest(validationresult);
            }

            await _commandBus.Submit(createUserDto);
#pragma warning disable CA1062 // Validate arguments of public methods
            return Created(_uriHelperService.GetApiUrl($"api/users/{createUserDto.UserName}"), _mapper.Map<ViewUserDto>(createUserDto));
#pragma warning restore CA1062 // Validate arguments of public methods
        }

        /// <summary>
        /// Generates JWT token for user.
        /// </summary>
        /// <param name="credentials">Credentials model.</param>
        /// <response code="200">When token generated.</response>
        /// <response code="404">When authentication failed.</response>
        [HttpPost]
        [Route("Auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUserDto credentials)
        {
            var validationResult = await _authenticateUserDtoValidator.ValidateAsync(credentials);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            if (await _userService.AuthenticateUser(credentials))
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                return Ok(_jwtService.GenerateToken(credentials.UserName, Roles.Admin));
#pragma warning restore CA1062 // Validate arguments of public methods
            }

            return BadRequest(ValidationResultFactory(nameof(credentials), credentials, UserControllerResources.AuthenticationFailed));
        }
    }
}
