using Airbnb.Application.Requests.Users.Commands.UpdateUser;
using AirbnbHousings.Models;
using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbHousings.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateUserCommand>(model);
            command.UserId = this.UserId;
            await Mediator.Send(command,cancellationToken);
            return Ok();
        }
    }
}
