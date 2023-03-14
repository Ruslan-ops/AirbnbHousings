using Airbnb.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly AirbnbContext _dbContext;
        private readonly IJwtService _jwtService;

        public ForgotPasswordCommandHandler(AirbnbContext dbContext, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var normEmail = request.Email.ToUpper();
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.NormEmail == normEmail, cancellationToken);
            if (user != null)
            {
                var refreshToken = _jwtService.GenerateRandomToken();
                user.RefreshPasswordToken = refreshToken;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return refreshToken;
            }
            throw new ValidationException(new ValidationFailure[] { new ValidationFailure { ErrorMessage = "user with the email does not exist" } });

        }
    }
}
