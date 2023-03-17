using Airbnb.Application.Requests.Users.Commands.AddUserPhoto;
using Airbnb.Application.Requests.Users.Commands.ChangeEmail;
using Airbnb.Application.Requests.Users.Commands.DeleteUserPhoto;
using Airbnb.Application.Requests.Users.Commands.UpdateUser;
using Airbnb.Application.Requests.Users.Queries.GetUser;
using Web.Models;
using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Web.Controllers
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

        [HttpDelete("photo/delete")]
        public async Task<IActionResult> DeleteUserPhoto([FromBody] DeleteUserPhotoDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteUserPhotoCommand>(model);
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

        [AllowAnonymous]
        [HttpGet("show")]
        public async Task<ActionResult<string>> GetUser(int? userId)
        {
            var vm = await Mediator.Send(new GetUserQuery { Userid = userId });
            return Ok(JsonSerializer.Serialize(vm));
        }
    }
}
