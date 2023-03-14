using Airbnb.Application.Requests.Users.Commands.AddUserPhoto;
using Airbnb.Application.Requests.Users.Commands.ChangeEmail;
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

        [HttpPost("photo/new")]
        public async Task<IActionResult> AddUserPhoto([FromForm] AddUserPhotoDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<AddUserPhotoCommand>(model);
            command.UserId = this.UserId;
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ChangeEmailCommand>(model);
            command.UserId = this.UserId;
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
