using Airbnb.Application.Common.Consts;
using Airbnb.Application.Common.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Requests.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
    {
        private readonly AirbnbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(AirbnbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .Include(u => u.UsersPhotos)
                .ThenInclude(up => up.Photo)
                .FirstOrDefaultAsync(u => u.UserId == request.Userid, cancellationToken);
            if (user == null)
                throw new CustomValidationException(ErrorMessages.UserNotFound);
            var vm = _mapper.Map<UserVm>(user);
            return vm;
        }
    }
}
