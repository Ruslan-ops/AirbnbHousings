using Airbnb.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        internal int? UserId => (User.Identity != null && User.Identity.IsAuthenticated) ?
                                Convert.ToInt32(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value) : null;
    }
}
