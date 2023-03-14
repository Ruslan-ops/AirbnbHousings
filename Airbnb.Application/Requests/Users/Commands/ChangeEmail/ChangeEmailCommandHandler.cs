using Airbnb.Application.Common.Consts;
using Airbnb.Application.Common.Exceptions;
using Airbnb.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Unit>
    {
        private readonly AirbnbContext _dbContext;
        private readonly IJwtService _jwtService;

        public ChangeEmailCommandHandler(AirbnbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<Unit> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            await ThrowOnEmailExists(request.Email);
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId  == request.UserId);
            if (user == null)
                throw new CustomValidationException(ErrorMessages.InvalidAuthentication);
            user.Email = request.Email;
            user.NormEmail = request.Email.ToUpper();
            user.IsEmailConfirmed = false;
            user.EmailVerificationToken = _jwtService.GenerateRandomToken();
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task ThrowOnEmailExists(string email)
        {
            var normEmail = email.ToUpper();
            var exists = await _dbContext.Users.AsNoTracking().AnyAsync(u => u.Email != null && u.NormEmail == normEmail);
            if (exists)
                throw new CustomValidationException("Email", ErrorMessages.EmailIsUsed);
        }
    }
}
