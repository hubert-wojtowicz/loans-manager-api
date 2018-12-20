﻿using System.Linq;
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
        private readonly ICommandDispatcher dispatecher;

        public UsersController(
            ApiSettings apiSettings,
            IMapper mapper,
            IJwtService jwtService,
            IUserService userService,
            ICommandDispatcher dispatecher
            )
        {
            this.apiSettings = apiSettings;
            this.mapper = mapper;
            this.jwtService = jwtService;
            this.userService = userService;
            this.dispatecher = dispatecher;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetManyUsersAsync([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "take")] int take = 15)
        {
            if (take > apiSettings.MaxNumberOfRecordToGet)
                return BadRequest(UserControllerResources.MaxNumberOfRecordToGetExceeded);

            var users = await userService.GetUsersAsync(offset, take);

            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUserAsync([FromBody]RegisterUserCommand createUserDto)
        {
            await dispatecher.Submit(createUserDto);
            return Created($"Users/{createUserDto.UserName}", mapper.Map<ViewUserDto>(createUserDto));
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
