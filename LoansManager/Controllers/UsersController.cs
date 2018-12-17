using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoansManager.Services.Dtos;
using LoansManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoansManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly int MaxNumberOfRecordToGet=100;

        public UsersController(
            IMapper mapper,
            IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetManyUsersAsync([FromQuery(Name = "offset")] int offset = 0, [FromQuery(Name = "take")] int take = 15)
        {
            if (take > MaxNumberOfRecordToGet)
                return BadRequest("To many records requested.");

            var users = await userService.GetUsersAsync(offset, take);

            if (users.Any())
                return Ok(users);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody]CreateUserDto createUserDto)
        {
            await userService.RegisterUserAsync(createUserDto);
            return Created($"Users/{createUserDto.UserName}", mapper.Map<ViewUserDto>(createUserDto));
        }
    }
}
