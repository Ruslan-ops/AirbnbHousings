using Airbnb.Application.Requests.Users.Commands.ForgotPassword;
using Airbnb.Application.Requests.Users.Commands.LoginEmail;
using Airbnb.Application.Requests.Users.Commands.RegisterEmail;
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

        

    }
}
