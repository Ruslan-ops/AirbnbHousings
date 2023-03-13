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

namespace Airbnb.Application.Requests.Auth.Commands.LoginEmail
{
    public class LoginEmailCommandHandler : IRequestHandler<LoginEmailCommand, string>
    {
        private readonly AirbnbContext _dbContext;
        private readonly IJwtService _jwtService;

        public LoginEmailCommandHandler(AirbnbContext dbContext, IJwtService jwtService) 
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginEmailCommand request, CancellationToken cancellationToken)
        {
            var normEmail = request.Email.ToUpper();
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.NormEmail == normEmail);
            if (user != null)
            {
                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword);
                if (isPasswordCorrect)
                {
                    var roles = await _dbContext.UsersRoles.AsNoTracking()
                        .Where(ur => ur.UserId == user.UserId)
                        .Include(ur => ur.Role)
                        .Select(ur => ur.Role)
                        .ToArrayAsync();

                    var token = _jwtService.Generate(user, roles);
                    return token;
                }
            }
            throw new ValidationException(new ValidationFailure[] { new ValidationFailure { ErrorMessage = "Wrong email or password" } });
        }
    }
}
