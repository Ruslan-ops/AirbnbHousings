using Airbnb.Application.Requests.Auth.Commands.RefreshPassword;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
    {
        private readonly AirbnbContext _dbContext;

        public ConfirmEmailCommandHandler(AirbnbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.EmailVerificationToken != null && u.EmailVerificationToken == request.Token, cancellationToken);
            if (user != null)
            {
                if (user.IsEmailConfirmed)
                    return Unit.Value;
                
                user.IsEmailConfirmed = true;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;                
            }
            throw new ValidationException(new ValidationFailure[] { new ValidationFailure { ErrorMessage = "user with the token not found" } });
        }
    }
}
