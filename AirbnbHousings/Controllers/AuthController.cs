using Airbnb.Application.Requests.Auth.Commands.ConfirmEmail;
using Airbnb.Application.Requests.Auth.Commands.ForgotPassword;
using Airbnb.Application.Requests.Auth.Commands.LoginEmail;
using Airbnb.Application.Requests.Auth.Commands.RefreshPassword;
using Airbnb.Application.Requests.Auth.Commands.RegisterEmail;
using AirbnbHousings.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AirbnbHousings.Controllers
{
    [Authorize]
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;

        public AuthController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromForm] RegisterEmailDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterEmailCommand>(model);
            var token = await Mediator.Send(command, cancellationToken);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromForm] LoginEmailDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginEmailCommand>(model);
            var token = await Mediator.Send(command, cancellationToken);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromForm] ForgotPasswordDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(model);
            var token = await Mediator.Send(command, cancellationToken);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("refresh-password")]
        public async Task<ActionResult> RefreshPassword([FromForm] RefreshPasswordDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RefreshPasswordCommand>(model);
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromForm] string Token, CancellationToken cancellationToken)
        {
            var command = new ConfirmEmailCommand { Token = Token};
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

    }
}
