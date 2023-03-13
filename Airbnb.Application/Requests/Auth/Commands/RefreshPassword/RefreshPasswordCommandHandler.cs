using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Airbnb.Application.Requests.Auth.Commands.RefreshPassword
{
    public class RefreshPasswordCommandHandler : IRequestHandler<RefreshPasswordCommand, Unit>
    {
        private readonly AirbnbContext _dbContext;

        public RefreshPasswordCommandHandler(AirbnbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RefreshPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.RefreshPasswordToken != null && u.RefreshPasswordToken == request.Token);
            if (user != null) 
            {
                var hashed = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                user.HashedPassword = hashed;
                user.PasswordChanged = DateTime.Now;
                user.RefreshPasswordToken = null;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Unit.Value;
            }
            throw new ValidationException(new ValidationFailure[] { new ValidationFailure { ErrorMessage = "user with the token not found" } });
        }
    }
}
