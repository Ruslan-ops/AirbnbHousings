using Airbnb.Application.Common.Consts;
using Airbnb.Application.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly AirbnbContext _dbContext;

        public UpdateUserCommandHandler(AirbnbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
            if (user == null)
                throw new CustomValidationException(ErrorMessages.InvalidAuthentication);
            
            user.ReceiveNews = request.RecieveNews;
            user.SexId = request.Sex;
            user.Description = request.Description;
            user.BornDate = request.BornDate;
            user.FirstName = request.FirstName;
            user.SecondName = request.SecondName;
            user.MiddleName = request.MiddleName;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
